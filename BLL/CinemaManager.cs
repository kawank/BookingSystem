using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BE;
using System.Data;
using Utility;
using System.Data.SqlClient;
using System.Reflection;

namespace BLL
{
    public class CinemaManager : BaseManager
    {

        public List<Cities> GetCityList()
        {
            List<Cities> cityList = Select<Cities>(StoredProcedureName.GET_CITYL_IST);
            return cityList;
        }
        public string AddScreen(Screen screen, out string message, out bool isSuccess)
        {
            DataTable dt = (from sc in screen.ScreenCategoryList
                            from sd in sc.SittingDetails
                            select new { ScreenSeatingCategory = sc.ScreenSeatingCategory, RowNumber = sd.RowNumber, ColumnNumber = sd.ColumnNumber, CellValue = sd.SeatText }).ToArray().ConvertIntoDataTable();

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@CinemaId", Value = screen.CinemaId });
            param.Add(new SqlParameter() { ParameterName = "@ScreenName", Value = screen.ScreenName });
            param.Add(new SqlParameter() { ParameterName = "@TypeTable_SeatConfiguration", Value = dt });
            param.Add(new SqlParameter() { ParameterName = "@Message", Value = "", Direction = ParameterDirection.Output, Size = 256 });
            param.Add(new SqlParameter() { ParameterName = "@IsSuccess", Value = false, Direction = ParameterDirection.Output, SqlDbType = SqlDbType.Bit });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int result = Insert(StoredProcedureName.ADD_SCREEN, param, out retParam);
            string outputParameter = retParam.First().Value.ToStringSafe();
            message = retParam[0].Value.ToStringSafe();
            isSuccess = retParam[1].Value.ToStringSafe() == "0" ? false : true;
            return outputParameter;

        }

        public string SaveScreen(Screen screen, out string message, out bool isSuccess)
        {
            DataTable dt = (from sc in screen.ScreenCategoryList
                            from sd in sc.SittingDetails
                            select new { ScreenSeatingCategory = sc.ScreenSeatingCategory, RowNumber = sd.RowNumber, ColumnNumber = sd.ColumnNumber, CellValue = sd.SeatText }).ToArray().ConvertIntoDataTable();

            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@CinemaId", Value = screen.CinemaId });
            param.Add(new SqlParameter() { ParameterName = "@ScreenName", Value = screen.ScreenName });
            param.Add(new SqlParameter() { ParameterName = "@TypeTable_SeatConfiguration", Value = dt });
            param.Add(new SqlParameter() { ParameterName = "@Message", Value = "", Direction = ParameterDirection.Output, Size = 256 });
            param.Add(new SqlParameter() { ParameterName = "@IsSuccess", Value = "", Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@ScreenId", Value = screen.ScreenId });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int result = Insert(StoredProcedureName.SAVE_SCREEN, param, out retParam);
            string outputParameter = retParam.First().Value.ToStringSafe();
            message = retParam[0].Value.ToStringSafe();
            isSuccess = retParam[1].Value.ToStringSafe() == "0" ? false : true;
            return outputParameter;

        }

        public Screen GetScreen(int screenId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            Screen result = null;
            param.Add(new SqlParameter() { ParameterName = "@ScreenId", Value = screenId });
            DataSet dsResult = Select(StoredProcedureName.GET_SCREEN_BY_ID, param);
            if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                result = dsResult.Tables[0].Rows[0].ConvertToEntity<Screen>();
                if (result != null && dsResult.Tables.Count > 1 && dsResult.Tables[1].Rows.Count > 0)
                {
                    result.ScreenCategoryList = dsResult.Tables[1].ConvertToList<ScreenCategory>();
                    if (result.ScreenCategoryList != null && result.ScreenCategoryList.Count > 0 && dsResult.Tables.Count > 2 && dsResult.Tables[2].Rows.Count > 0)
                    {
                        foreach (ScreenCategory item in result.ScreenCategoryList)
                        {
                            string filter = "ScreenSeatingCategoryId=" + item.ScreenSeatingCategoryId;
                            item.SittingDetails = dsResult.Tables[2].Select(filter).ConvertToList<SittingLayout>();
                        }
                    }
                }
            }
            return result;
        }

        public List<Screen> GetCinemaScreens(int cinemaId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@CinemaId", Value = cinemaId });
            return Select<Screen>(StoredProcedureName.GET_CINEMA_SCREEN, param);
        }
        public bool DeleteScreen(int screenId, out bool isSuccess, out string message)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@ScreenId", Value = screenId });
            param.Add(new SqlParameter() { ParameterName = "@Message", Value = "", Direction = ParameterDirection.Output, Size = 256 });
            param.Add(new SqlParameter() { ParameterName = "@IsSuccess", Value = "", Direction = ParameterDirection.Output, Size = 10 });
            List<SqlParameter> retParam = new List<SqlParameter>();
            int result = Insert(StoredProcedureName.DELETE_SCREEN, param, out retParam);
            string outputParameter = retParam.First().Value.ToStringSafe();
            message = retParam[0].Value.ToStringSafe();
            isSuccess = retParam[1].Value.ToStringSafe() == "0" ? false : true;
            return isSuccess;
        }
        //public string AddCinema(Cinema objCinema)
        public Message AddCinema(Cinema objCinema)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@CinemaId", Value = 0 });
            param.Add(new SqlParameter() { ParameterName = "@CinemaName", Value = objCinema.CinemaName });
            param.Add(new SqlParameter() { ParameterName = "@Phone", Value = objCinema.Phone });
            param.Add(new SqlParameter() { ParameterName = "@Email", Value = objCinema.Email });
            param.Add(new SqlParameter() { ParameterName = "@Website", Value = objCinema.WebSite });
            param.Add(new SqlParameter() { ParameterName = "@ContactPerson", Value = objCinema.ContactPerson });
            param.Add(new SqlParameter() { ParameterName = "@IsMultiplex", Value = objCinema.IsMultiplex });
            param.Add(new SqlParameter() { ParameterName = "@CityId", Value = objCinema.CityId });
            param.Add(new SqlParameter() { ParameterName = "@CreatedBy", Value = objCinema.CreatedBy });
            param.Add(new SqlParameter() { ParameterName = "@CreatedDate", Value = objCinema.CreatedDate });
            param.Add(new SqlParameter() { ParameterName = "@UserId", Value = objCinema.UserId });
            param.Add(new SqlParameter() { ParameterName = "@Address", Value = objCinema.Address });
            param.Add(new SqlParameter() { ParameterName = "@MessageCode", Value = objCinema.MessageCode, Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDiscrition", Value = objCinema.MessageDescription, Direction = ParameterDirection.Output, Size = 100 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int result = Insert(StoredProcedureName.INSERT_CINEMA, param, out retParam);
           

            return new Message { MessageCode = (bool)retParam.ElementAtOrDefault(0).Value, MessageDescription = retParam.ElementAtOrDefault(1).Value.ToStringSafe() };
            //  return outputParameter;

        }


        public Message UpdateCinema(Cinema objCinema)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@CinemaId", Value = objCinema.CinemaId });
            param.Add(new SqlParameter() { ParameterName = "@CinemaName", Value = objCinema.CinemaName });
            param.Add(new SqlParameter() { ParameterName = "@Phone", Value = objCinema.Phone });
            param.Add(new SqlParameter() { ParameterName = "@Email", Value = objCinema.Email });
            param.Add(new SqlParameter() { ParameterName = "@Website", Value = objCinema.WebSite });
            param.Add(new SqlParameter() { ParameterName = "@ContactPerson", Value = objCinema.ContactPerson });
            param.Add(new SqlParameter() { ParameterName = "@IsMultiplex", Value = objCinema.IsMultiplex });
            param.Add(new SqlParameter() { ParameterName = "@CityId", Value = objCinema.CityId });
            param.Add(new SqlParameter() { ParameterName = "@ModifiedBy", Value = objCinema.ModifiedBy });
            param.Add(new SqlParameter() { ParameterName = "@ModifiedDate", Value = objCinema.ModifiedDate });
            param.Add(new SqlParameter() { ParameterName = "@UserId", Value = objCinema.UserId });
            param.Add(new SqlParameter() { ParameterName = "@Address", Value = objCinema.Address });
            param.Add(new SqlParameter() { ParameterName = "@MessageCode", Value = objCinema.MessageCode, Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDiscrition", Value = objCinema.MessageDescription, Direction = ParameterDirection.Output, Size = 1000 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int result = Insert(StoredProcedureName.UPDATE_CINEMA, param, out retParam);

            return new Message { MessageCode = (bool)retParam.ElementAtOrDefault(0).Value, MessageDescription = retParam.ElementAtOrDefault(1).Value.ToStringSafe() };
        }

        public Message DeleteCinema(int cinemaId, int userId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@CinemaId", Value = cinemaId });
            param.Add(new SqlParameter() { ParameterName = "@UserId", Value = userId });
            param.Add(new SqlParameter() { ParameterName = "@MessageCode", Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDescription", Direction = ParameterDirection.Output, Size = 100 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int isDeleted = Delete(param, StoredProcedureName.DELETE_CINEMA, out retParam);

            bool msgCode = false;

            if (String.Equals(retParam.ElementAtOrDefault(0).Value, "1"))
            {
                msgCode = true;
            }

            return new Message { MessageCode = msgCode, MessageDescription = retParam.ElementAtOrDefault(1).Value.ToStringSafe() };
        }

        public List<Cinema> GetCinema(int userId)
        {
            List<Cinema> cinemaList = null;
            if (userId == 0)
            {
                cinemaList = Select<Cinema>(StoredProcedureName.GET_CINEMA_LIST);
            }
            else
            {
                List<SqlParameter> param = new List<SqlParameter>();
                param.Add(new SqlParameter() { ParameterName = "@UserId", Value = userId });
                cinemaList = Select<Cinema>(StoredProcedureName.GET_CINEMA_LIST, param);
            }

            return cinemaList;
        }


        static T Cast<T>(object obj, T type)
        {
            return (T)obj;
        }

        public List<JsonEvent> GetEvents(string startDate, string endDate, int screenId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@EventStartDate", Value = startDate });
            param.Add(new SqlParameter() { ParameterName = "@EventEndDate", Value = endDate });
            param.Add(new SqlParameter() { ParameterName = "@ScreenId", Value = screenId });
            return Select<JsonEvent>(StoredProcedureName.GET_EVENTS, param);
        }
        public List<MovieKeyValue> GetMoviesKeyValue()
        {
            return Select<MovieKeyValue>(StoredProcedureName.GET_MOVIES_KEY_VALUE);
        }
        public List<ScreenCategory> GetScreenCategories(int screenId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@ScreenId", Value = screenId });
            return Select<ScreenCategory>(StoredProcedureName.GET_SCREEN_CATEGORIES, param);
        }
        public Dictionary<string, object> SaveEvent(string eventStartDate, string eventEndDate, string startTime, string endTime, int movieId, int screenId, int eventId, string categoryRates, string skipDays, string skipDates, bool isExcludeExistingEvents, bool isOverwriteExistingEvents, bool isPartiallyEdit)
        {
            Dictionary<string, object> result = null;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@EventStartDate", Value = eventStartDate });
            param.Add(new SqlParameter() { ParameterName = "@EventEndDate", Value = eventEndDate });
            param.Add(new SqlParameter() { ParameterName = "@StartTime", Value = startTime });
            param.Add(new SqlParameter() { ParameterName = "@EndTime", Value = endTime });
            param.Add(new SqlParameter() { ParameterName = "@MovieId", Value = movieId });
            param.Add(new SqlParameter() { ParameterName = "@ScreenId", Value = screenId });
            param.Add(new SqlParameter() { ParameterName = "@EventId", Value = eventId });
            param.Add(new SqlParameter() { ParameterName = "@CatRates", Value = categoryRates });
            param.Add(new SqlParameter() { ParameterName = "@SkipDays", Value = skipDays });
            param.Add(new SqlParameter() { ParameterName = "@SkipDates", Value = skipDates });
            param.Add(new SqlParameter() { ParameterName = "@IsExcludeExistingEvents", Value = isExcludeExistingEvents });
            param.Add(new SqlParameter() { ParameterName = "@IsOverwriteExistingEvents", Value = isOverwriteExistingEvents });
            param.Add(new SqlParameter() { ParameterName = "@IsPartiallyEdit", Value = isPartiallyEdit });
            param.Add(new SqlParameter() { ParameterName = "@IsSucess", Value = false, Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@Message", Value = string.Empty, Direction = ParameterDirection.Output, Size = 100 });
            param.Add(new SqlParameter() { ParameterName = "@ResultText", Value = string.Empty, Direction = ParameterDirection.Output, Size = 100000000 });
            List<SqlParameter> retParam = new List<SqlParameter>();
            Insert(StoredProcedureName.SAVE_EVENT, param, out retParam);
            if (retParam != null)
            {
                result = new Dictionary<string, object> { { retParam.FirstOrDefault().ParameterName.Replace("@", ""), retParam.FirstOrDefault().Value.ToBooleanSafe() }, { retParam.ElementAtOrDefault(1).ParameterName.Replace("@", ""), retParam.ElementAtOrDefault(1).Value.ToStringSafe() }, { retParam.ElementAtOrDefault(2).ParameterName.Replace("@", ""), retParam.ElementAtOrDefault(2).Value.ToStringSafe() } };
            }
            else
            {
                result = new Dictionary<string, object> { { "IsSucess", false }, { "Message", "Unable to save the event." }, { "ResultXML", string.Empty } };
            }
            return result;
        }
        public Dictionary<string, object> DeleteEvent(string eventStartDate, bool deleteTillEnd, int eventId, bool isPartiallyDelete)
        {
            Dictionary<string, object> result = null;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@EventStartDate", Value = eventStartDate });
            param.Add(new SqlParameter() { ParameterName = "@DeleteTillEnd", Value = deleteTillEnd });
            param.Add(new SqlParameter() { ParameterName = "@EventId", Value = eventId });
            param.Add(new SqlParameter() { ParameterName = "@IsPartiallyDelete", Value = isPartiallyDelete });
            param.Add(new SqlParameter() { ParameterName = "@IsSucess", Value = false, Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@Message", Value = string.Empty, Direction = ParameterDirection.Output, Size = 1000 });
            List<SqlParameter> retParam = new List<SqlParameter>();
            Insert(StoredProcedureName.DELETE_EVENT, param, out retParam);
            if (retParam != null)
            {
                result = new Dictionary<string, object> { { retParam.FirstOrDefault().ParameterName.Replace("@", ""), retParam.FirstOrDefault().Value.ToBooleanSafe() }, { retParam.ElementAtOrDefault(1).ParameterName.Replace("@", ""), retParam.ElementAtOrDefault(1).Value.ToStringSafe() } };
            }
            else
            {
                result = new Dictionary<string, object> { { "IsSucess", false }, { "Message", "Unable to delete the event." } };
            }
            return result;
        }

        public ScreenDetailsForBooking GetShowDetailsForBooking(long showId, byte? noOfTickets)
        {
            if (noOfTickets == null) { noOfTickets = 1; }
            ScreenDetailsForBooking result = new ScreenDetailsForBooking();
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@ShowId", Value = showId });
            DataSet dsResult = Select(StoredProcedureName.GET_SHOW_DETAILS_FOR_BOOKING, param);
            if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
            {
                result = dsResult.Tables[0].Rows[0].ConvertToEntity<ScreenDetailsForBooking>();
                if (result != null && dsResult.Tables.Count > 1 && dsResult.Tables[1].Rows.Count > 0)
                {
                    result.ScreenCategoryList = dsResult.Tables[1].ConvertToList<ScreenCategory>();
                    if (result.ScreenCategoryList != null && result.ScreenCategoryList.Count > 0 && dsResult.Tables.Count > 2 && dsResult.Tables[2].Rows.Count > 0)
                    {
                        foreach (ScreenCategory item in result.ScreenCategoryList)
                        {
                            string filter = "ScreenSeatingCategoryId=" + item.ScreenSeatingCategoryId;
                            item.SittingDetails = dsResult.Tables[2].Select(filter).ConvertToList<SittingLayout>();
                        }
                    }
                }
                result.NoOfSeatsSelected = (byte)noOfTickets;
            }
            return result;
        }

        public Dictionary<string, object> AddBookingInfo(long showId, byte noOfSeatsSelected, string configurationIds, int customerId, float totalAmount)
        {
            Dictionary<string, object> result = null;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@ShowId", Value = showId });
            param.Add(new SqlParameter() { ParameterName = "@CustomerId", Value = customerId });
            param.Add(new SqlParameter() { ParameterName = "@NoOfSeats", Value = noOfSeatsSelected });
            param.Add(new SqlParameter() { ParameterName = "@TotalAmount", Value = totalAmount });
            param.Add(new SqlParameter() { ParameterName = "@ConfigurationIds", Value = configurationIds });
            param.Add(new SqlParameter() { ParameterName = "@IsSuccess", Value = false, Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@Message", Value = string.Empty, Direction = ParameterDirection.Output, Size = 1000 });
            param.Add(new SqlParameter() { ParameterName = "@BookingInfoId", Value = 0, Direction = ParameterDirection.Output, Size = 20 });
            List<SqlParameter> retParam = new List<SqlParameter>();
            Insert(StoredProcedureName.ADD_BOOKING_INFO, param, out retParam);
            if (retParam != null)
            {
                result = new Dictionary<string, object> { { retParam.FirstOrDefault().ParameterName.Replace("@", ""), retParam.FirstOrDefault().Value.ToBooleanSafe() }, { retParam.ElementAtOrDefault(1).ParameterName.Replace("@", ""), retParam.ElementAtOrDefault(1).Value.ToStringSafe() }, { retParam.ElementAtOrDefault(2).ParameterName.Replace("@", ""), retParam.ElementAtOrDefault(2).Value.ToDecimalSafe() } };
            }
            else
            {
                result = new Dictionary<string, object> { { "IsSucess", false }, { "Message", "Unable to add information this time." } };
            }
            return result;
        }

        public Dictionary<string, object> MovieTicketConfirmation(long uniqueId, bool status, string transactionId, DateTime? paymentDate = null, float? amount = null)
        {
            Dictionary<string, object> result = null;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@BookingInfoId", Value = uniqueId });
            param.Add(new SqlParameter() { ParameterName = "@TransactionId", Value = transactionId });
            param.Add(new SqlParameter() { ParameterName = "@Amount", Value = amount });
            if (paymentDate != null)
            {
                param.Add(new SqlParameter() { ParameterName = "@PaymentDate", Value = paymentDate });
            }
            param.Add(new SqlParameter() { ParameterName = "@Status", Value = status });
            param.Add(new SqlParameter() { ParameterName = "@IsSuccess", Value = false, Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@Message", Value = string.Empty, Direction = ParameterDirection.Output, Size = 1000 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            DataSet dsResult = Select(StoredProcedureName.UPDATE_BOOKING_INFO, param, out retParam);
            if (retParam != null)
            {
                result = new Dictionary<string, object> { { retParam.FirstOrDefault().ParameterName.Replace("@", ""), retParam.FirstOrDefault().Value.ToBooleanSafe() }, { retParam.ElementAtOrDefault(1).ParameterName.Replace("@", ""), retParam.ElementAtOrDefault(1).Value.ToStringSafe() } };
                if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                {
                    
                    for (int counter = 0; counter < dsResult.Tables[0].Columns.Count; counter++)
                    {
                        result.Add(dsResult.Tables[0].Columns[counter].ColumnName, dsResult.Tables[0].Rows[0][counter].ToString());
                    }
                }

            }
            else
            {
                result = new Dictionary<string, object> { { "IsSucess", false }, { "Message", "Unable to update informationm this time." } };
            }
            return result;
        }

        public CinemaShows GetShowDetailsByCinemaId(int cinemaId, DateTime fromDate, DateTime toDate)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@CinemaId", Value = cinemaId });
            param.Add(new SqlParameter() { ParameterName = "@FromDate", Value = fromDate });
            param.Add(new SqlParameter() { ParameterName = "@ToDate", Value = toDate });


            DataSet ds = Select(StoredProcedureName.GET_SHOW_LIST_BY_CINEMA_ID, param);

            CinemaShows showDetails = new CinemaShows();
            List<DayWiseShowDetails> showList = new List<DayWiseShowDetails>();
            List<CinemaMovieDetails> movieList = null;
            DateTime currentDate = fromDate;
            int movieId = 0;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                while (currentDate <= toDate)
                {
                    DayWiseShowDetails currentDayDetails = new DayWiseShowDetails();
                    currentDayDetails.ShowDate = currentDate;

                    movieList = new List<CinemaMovieDetails>();
                    foreach (DataRow currentMovie in ds.Tables[0].Rows)
                    {
                        if (Convert.ToDateTime(currentMovie["ShowDate"]).ToShortDateString() == currentDate.ToShortDateString())
                        {
                            movieId = currentMovie["MovieId"].ToIntSafe();

                            if (movieList.Select(c => c.MovieId).Contains(movieId))
                            {
                                continue;
                            }
                            string movieName = currentMovie["MovieName"].ToStringSafe();
                            CinemaMovieDetails movie = new CinemaMovieDetails { MovieId = movieId, MovieName = movieName };


                            string filter = "MovieId=" + currentMovie["MovieId"].ToString() + " and " + "ShowDate='" + currentDayDetails.ShowDate + "'";
                            movie.ShowList = ds.Tables[0].Select(filter).ConvertToList<ShowDetails>();

                            movieList.Add(movie);
                        }
                    }

                    if (movieList != null)
                    {
                        currentDayDetails.MovieList = movieList;
                    }

                    showList.Add(currentDayDetails);


                    TimeSpan duration = new TimeSpan(1, 0, 0, 0);
                    currentDate = currentDate.Add(duration);

                }

            }
            if (showList != null)
            {
                showDetails.ShowList = showList;
            }

            return showDetails;

        }

    }
}
