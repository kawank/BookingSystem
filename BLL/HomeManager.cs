using BE;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility;

namespace BLL
{
   public class HomeManager:BaseManager
    {

       public List<MovieListHome> GetMovieListHome(short? CityId=null)
       {
           if (CityId == 0)
           {
               CityId = null;
           }
           List<SqlParameter> param = new List<SqlParameter>();
           param.Add(new SqlParameter() { ParameterName = "@CityId", Value = CityId });
           List<MovieListHome> movieList = Select<MovieListHome>(StoredProcedureName.GET_MOVIES_FORHOME, param);
           return movieList;
       }

       public List<CinemaListHome> GetCinemaListHome(short? CityId = null)
       {
           if (CityId == 0)
           {
               CityId = null;
           }
           List<SqlParameter> param = new List<SqlParameter>();
           param.Add(new SqlParameter() { ParameterName = "@CityId", Value = CityId });
           List<CinemaListHome> cinemaList = Select<CinemaListHome>(StoredProcedureName.GET_CINEMA_FORHOME_BYCITYID,param);
           return cinemaList;
       }

       public List<MovieListHome> GetMovieListOpeningThisWeek()
       {
          
           List<MovieListHome> movieList = Select<MovieListHome>(StoredProcedureName.GET_MOVIES_OPENINGTHISWEEK);
           return movieList;

       }

       public List<MovieListHome> GetMovieListComeingSoon()
       {
          
           List<MovieListHome> movieList = Select<MovieListHome>(StoredProcedureName.GET_MOVIES_COMINGSOON);
           return movieList;

       }
    }
}
