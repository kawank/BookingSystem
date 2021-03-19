using BE;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BLL
{
    public class MovieManager : BaseManager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public Message AddMovie(Movie movie)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@MovieName", Value = movie.MovieName });
            param.Add(new SqlParameter() { ParameterName = "@Cast", Value = movie.Cast });
            param.Add(new SqlParameter() { ParameterName = "@Certificate", Value = movie.Certificate });
            param.Add(new SqlParameter() { ParameterName = "@Description", Value = movie.Description });
            param.Add(new SqlParameter() { ParameterName = "@director", Value = movie.Director });
            param.Add(new SqlParameter() { ParameterName = "@hero", Value = movie.Hero });
            param.Add(new SqlParameter() { ParameterName = "@heroine", Value = movie.Heroine });
            param.Add(new SqlParameter() { ParameterName = "@movieImage", Value = movie.MovieImage });
            param.Add(new SqlParameter() { ParameterName = "@releaseDate", Value = movie.ReleaseDate });
            param.Add(new SqlParameter() { ParameterName = "@trailerUrl", Value = movie.TrailerUrl });
            param.Add(new SqlParameter() { ParameterName = "@Musician", Value = movie.Musician });
            param.Add(new SqlParameter() { ParameterName = "@Duration", Value = movie.Duration });
            param.Add(new SqlParameter() { ParameterName = "@MovieGenre", Value = movie.SelectedGenre });

            param.Add(new SqlParameter() { ParameterName = "@MessageCode", Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDescription", Direction = ParameterDirection.Output, Size = 100 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int isInserted = Insert(StoredProcedureName.INSERT_MOVIE, param, out retParam);

            bool msgCode = false;

            if (String.Equals(retParam.ElementAtOrDefault(0).Value, "1"))
            {
                msgCode = true;
            }

            return new Message { MessageCode = msgCode, MessageDescription = retParam.ElementAtOrDefault(1).Value.ToStringSafe() };

        }
        /// <summary>
        /// Get Genre list provide list of all the genre exist in the database 
        /// </summary>
        /// <returns></returns>
        public List<GenreKeyValuePair> GetGenreList()
        {
            List<GenreKeyValuePair> mgener = Select<GenreKeyValuePair>(StoredProcedureName.GET_GENRE_LIST);
            return mgener;
        }


        public Movie GetMovieDetailsById(int movieId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@MovieId", Value = movieId });

            List<Movie> movieList = Select<Movie>(StoredProcedureName.GET_MOVIE_BY_ID, param);
            return movieList.FirstOrDefault();
        }


        public Movie GetMovieShowDetailsById(int movieId, short cityId)
        {
            TimeSpan duration;
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@MovieId", Value = movieId });
            param.Add(new SqlParameter() { ParameterName = "@CityId", Value = movieId });

            DataSet ds = Select(StoredProcedureName.GET_MOVIE_SHOW_BY_ID, param);

            Movie movie = null;
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                DataRow row = ds.Tables[0].Rows[0];
                TimeSpan.TryParse(row["Duration"].ToStringSafe(), out duration);

                List<Movie> movieList = ds.Tables[0].ConvertToList<Movie>();
                movie = movieList.FirstOrDefault();

                List<CinemaDetails> cinemaList = null;
                if (ds.Tables.Count > 1 && ds.Tables[1].Rows.Count > 0)
                {
                    cinemaList = new List<CinemaDetails>();
                    foreach (DataRow currentCinema in ds.Tables[1].Rows)
                    {
                        int cinemaId = currentCinema["CinemaId"].ToIntSafe();

                        if (cinemaList.Select(c => c.CinemaId).Contains(cinemaId))
                        {
                            continue;
                        }
                        string cinemaName = currentCinema["CinemaName"].ToStringSafe();
                        CinemaDetails cinema = new CinemaDetails { CinemaId = cinemaId, CinemaName = cinemaName };


                        string filter = "CinemaId=" + currentCinema["CinemaId"].ToString();
                        cinema.ShowList = ds.Tables[1].Select(filter).ConvertToList<ShowDetails>();

                        cinemaList.Add(cinema);
                    }

                }



                if (cinemaList != null)
                {
                    movie.CinemaList = cinemaList;
                }

            }
            return movie;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="movie"></param>
        /// <returns></returns>
        public Message UpdateMovie(Movie movie)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@MovieId", Value = movie.MovieId });
            param.Add(new SqlParameter() { ParameterName = "@MovieName", Value = movie.MovieName });
            param.Add(new SqlParameter() { ParameterName = "@Cast", Value = movie.Cast });
            param.Add(new SqlParameter() { ParameterName = "@Certificate", Value = movie.Certificate });
            param.Add(new SqlParameter() { ParameterName = "@Description", Value = movie.Description });
            param.Add(new SqlParameter() { ParameterName = "@Director", Value = movie.Director });
            param.Add(new SqlParameter() { ParameterName = "@Hero", Value = movie.Hero });
            param.Add(new SqlParameter() { ParameterName = "@Heroine", Value = movie.Heroine });
            param.Add(new SqlParameter() { ParameterName = "@MovieImage", Value = movie.MovieImage });
            param.Add(new SqlParameter() { ParameterName = "@ReleaseDate", Value = movie.ReleaseDate });
            param.Add(new SqlParameter() { ParameterName = "@TrailerUrl", Value = movie.TrailerUrl });
            param.Add(new SqlParameter() { ParameterName = "@Musician", Value = movie.Musician });
            param.Add(new SqlParameter() { ParameterName = "@Duration", Value = movie.Duration });
            param.Add(new SqlParameter() { ParameterName = "@MovieGenre", Value = movie.SelectedGenre });

            param.Add(new SqlParameter() { ParameterName = "@MessageCode", Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDescription", Direction = ParameterDirection.Output, Size = 100 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int isUpdated = Update(param, StoredProcedureName.UPDATE_MOVIE, out retParam);

            bool msgCode = false;

            if (String.Equals(retParam.ElementAtOrDefault(0).Value, "1"))
            {
                msgCode = true;
            }

            return new Message { MessageCode = msgCode, MessageDescription = retParam.ElementAtOrDefault(1).Value.ToStringSafe() };

        }




        public string IsMovieAlreadyExists(string movieName)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@MovieName", Value = movieName });
            string resultName = SelectScalar(StoredProcedureName.IS_MOVIE_EXISTS_BY_MOVIE_NAME, param).ToStringSafe();
            return resultName;
        }
        public List<Movie> GetMoviesByCriteria(string movieName, int offset)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@MovieName", Value = movieName });
            param.Add(new SqlParameter() { ParameterName = "@offset", Value = offset });
            List<Movie> lstMovies = Select<Movie>(StoredProcedureName.GET_MOVIE_LIST_BY_CRITERIA, param);
            return lstMovies;


        }

        public List<MovieSearchKey> GetMovieList()
        {
            List<MovieSearchKey> lstMovies = Select<MovieSearchKey>(StoredProcedureName.GET_MOVIE_LIST);
            return lstMovies;


        }


        public Shows GetShowDetailsByCriteria(int movieId, short cityId, DateTime fromDate, DateTime toDate)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@MovieId", Value = movieId });
            param.Add(new SqlParameter() { ParameterName = "@CityId", Value = cityId });
            param.Add(new SqlParameter() { ParameterName = "@FromDate", Value = fromDate });
            param.Add(new SqlParameter() { ParameterName = "@ToDate", Value = toDate });


            DataSet ds = Select(StoredProcedureName.GET_SHOW_LIST, param);

            Shows showDetails = new Shows();
            List<DayWiseShowDetails> showList = new List<DayWiseShowDetails>();
            List<CinemaDetails> cinemaList = null;
            DateTime currentDate = fromDate;
            int cinemaId = 0;

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                while (currentDate <= toDate)
                {
                    DayWiseShowDetails currentDayDetails = new DayWiseShowDetails();
                    currentDayDetails.ShowDate = currentDate;

                    cinemaList = new List<CinemaDetails>();
                    foreach (DataRow currentCinema in ds.Tables[0].Rows)
                    {
                        if (Convert.ToDateTime(currentCinema["ShowDate"]).ToShortDateString() == currentDate.ToShortDateString())
                        {
                            cinemaId = currentCinema["CinemaId"].ToIntSafe();

                            if (cinemaList.Select(c => c.CinemaId).Contains(cinemaId))
                            {
                                continue;
                            }
                            string cinemaName = currentCinema["CinemaName"].ToStringSafe();
                            CinemaDetails cinema = new CinemaDetails { CinemaId = cinemaId, CinemaName = cinemaName };


                            string filter = "CinemaId=" + currentCinema["CinemaId"].ToString() + " and " + "ShowDate='" + currentDayDetails.ShowDate + "'";
                            cinema.ShowList = ds.Tables[0].Select(filter).ConvertToList<ShowDetails>();

                            cinemaList.Add(cinema);
                        }
                    }

                    if (cinemaList != null)
                    {
                        currentDayDetails.CinemaList = cinemaList;
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

        public Message Delete(int movieId)
        {
            List<SqlParameter> param = new List<SqlParameter>();
            param.Add(new SqlParameter() { ParameterName = "@movieId", Value = movieId });
                        param.Add(new SqlParameter() { ParameterName = "@MessageCode", Direction = ParameterDirection.Output, Size = 10 });
            param.Add(new SqlParameter() { ParameterName = "@MessageDescription", Direction = ParameterDirection.Output, Size = 100 });

            List<SqlParameter> retParam = new List<SqlParameter>();
            int isDeleted = Delete(param, StoredProcedureName.DELETE_MOVIE, out retParam);

            bool msgCode = false;

            if (String.Equals(retParam.ElementAtOrDefault(0).Value, "1"))
            {
                msgCode = true;
            }

            return new Message { MessageCode = msgCode, MessageDescription = retParam.ElementAtOrDefault(1).Value.ToStringSafe() };
        }
    }
}
