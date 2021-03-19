using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{

    /// <summary>
    /// 
    /// </summary>
    public class StoredProcedureName
    {

        // GET 
        public const string GET_ADMIN_BY_USERNAME_AND_PASSWORD = "GetAdminByUserNameAndPassword";
        public const string GET_ADMIN_USER_LIST = "GetAdminUserList";
        public const string GET_ADMIN_USER_DETAILS_BY_USER_ID = "GetAdminUserDetailsByUserId";
        public const string GET_SCREEN_BY_ID = "GetScreenById";
        public const string GET_CINEMA_SCREEN = "GetCinemaScreen";
        public const string GET_ROLE_LIST = "GetRoleList";
        public const string GET_USERS_BY_ROLE = "GetUserByRole";
        public const string GET_CITYL_IST = "GetCityList";
        public const string GET_CINEMA_LIST = "GetCinemaList";
        public const string GET_CINEMA_LIST_BY_CINEMA_NAME_AND_CITY_ID = "GetCinemaListByCinemaNameAndCityId";
        public const string GET_SITE_USER_MENU_ITEMS = "GetSiteUserMenuItems";
        public const string GET_USER_BY_USERNAME_AND_PASSWORD = "GetUserByUserNameAndPassword";
        public const string GET_USER_LIST = "GetUserList";
        public const string GET_USER_PASSWORD_BY_USERNAME = "GetUserPasswordByUserName";
        public const string GET_GENRE_LIST = "GetGenreList";
        public const string GET_MOVIE_BY_ID = "GetMovieDetailsById";
        public const string GET_MOVIE_LIST_BY_CRITERIA = "GetMovieListByCriteria";


        //DELETE
        public const string DELETE_USER_ROLES = "DeleteUserRoles";
        public const string DELETE_ADMIN_USER = "DeleteAdminUser";
        public const string DELETE_SCREEN = "DeleteScreen";


        // INSERT
        public const string INSERT_CINEMA = "InsertCinema";
        public const string INSERT_MOVIE = "InsertMovie";
        public const string INSERT_SCREEN = "InsertScreen";
        public const string INSERT_USER = "InsertUser";


        //UPDATE
        public const string UPDATE_USER_ROLE = "UpdateUserRole";
        public const string UPDATE_ADMIN_USER_DETAILS = "UpdateAdminUserDetails";
        public const string UPDATE_CINEMA = "UpdateCinema";
        public const string UPDATE_MOVIE = "UpdateMovie";
        public const string UPDATE_USER = "UpdateUser";
        public const string UPDATE_USER_PASSWORD = "UpdateUserPassword";

        public const string ADD_SCREEN = "AddScreen";
        public const string SAVE_SCREEN = "SaveScreen";

        public const string IS_MOVIE_EXISTS_BY_MOVIE_NAME = "IsMovieExistsByMovieName";


    }

    /// <summary>
    /// 
    /// </summary>
    public class SessionKeys
    {
        public const string AUTHENTIC_USER = "AuthenticUser";
    }

    /// <summary>
    /// 
    /// </summary>
    public class ErrorMessages
    {
        public const string INVALID_USERNAME_AND_PASSWORD = "Invalid Username and Password";
        public const string REQUIRED_FIELDS_MESSAGE = "Fields marked (*) are mandatory.";
        public const string REQUIRED_SITTING_CATEGORY_NAME = "Sitting category name required at very first row.";
        public const string INVALID_SCREEN_INPUT = "Invalid Screen Input.";
        public const string INVALID_USERNAME = "Invalid Username";


    }

    /// <summary>
    /// 
    /// </summary>
    public class SuccessMessages
    {
        public const string USER_DELETED = "User deleted succesfully!";
        public const string PASSWORD_SENT_ON_MAIL = "We have sent an update password on your registered Email!";
    }

    /// <summary>
    /// 
    /// </summary>
    public class RoleKeys
    {
        public const int SUPER_ADMIN = 1;
        public const int CINEMA_OWNER = 2;
        public const int SITE_USER = 3;

    }


    /// <summary>
    /// 
    /// </summary>
    public class ConfigKeys
    {
        public const string CONNECTION_STRING = "ConnectionString";
        public const string FROM_DISPLAY = "FromDisplay";
        public const string FROM_EMAIL_ADDRESS = "FromEmailAddress";
        public const string CC_TO = "ccTo";
        public const string SMTP_SERVER_NAME = "smtpServerName";
        public const string MAIL_USER_NAME = "mailUserName";
        public const string MAIL_PASSWORD = "mailPassword";
        public const string PORT = "Port";
        public const string ENABLE_SSL = "EnableSSL";

    }

    public class EmailTemplates
    {
        public const string FORGOT_PASSWORD = "ForgotEmail.cshtml";

    }
    public class EmailSubjects
    {
        public const string FORGOT_PASSWORD = "Your New Password";
        public const string REGISTER = "Welcome to CB";
    }
    public class ToolTips
    {
        public const string PLEASE_ENTER_FIRST_NAME = "Please enter first name!";
        public const string PLEASE_ENTER_LAST_NAME = "Please enter last name!";
        public const string PLEASE_ENTER_USER_NAME = "Please enter username!";
        public const string PLEASE_SELECT_ROLE = "Please select Role!";
        public const string PLEASE_ENTER_MOBILE = "Please enter mobile!";
        public const string PLEASE_ENTER_PASSWORD = "Please enter Password!";
        public const string PLEASE_RE_ENTER_PASSWORD = "Please enter re password!";
        public const string PLEASE_ENTER_MOVIE_NAME = "Please enter movie name!";
        public const string PLEASE_ENTER_DIRECTOR = "Please enter director name!";
        public const string PLEASE_ENTER_MUSICIAN = "Please enter musician!";
        public const string PLEASE_ENTER_CAST = "Please enter Cast!";
        public const string PLEASE_ENTER_DESCRIPTION = "Please enter description!";
        public const string PLEASE_ENTER_ACTOR = "Please enter enter actor!";
        public const string PLEASE_ENETER_ACTORESS = "Please enter actoress!";
        public const string PLEASE_ENTER_RELEASE_DATE = "Please enter release date!";
        public const string PLEASE_ENTER_DURATION = "Please enter duration!";
        public const string PLEASE_ENTER_TRAILER = "Please enter trailer!";
        public const string PLEASE_SELECT_GENER = "Please select Gener!";
        public const string PLEASE_ENTER_PHONE = "Please enter phone!";
        public const string PLEASE_ENTER_EMAIL = "Please enter E-mail!";
        public const string PLEASE_WEB_SITE = "Please enter website!";
        public const string PLEASE_CONTACT_PERSON = "Please enter contact person!";
        public const string PLEASE_SELECT_CITY = "Please select City!";
        public const string PLEASE_ENTER_CINEMA_NAME = "Please enter cinema name!";
        public const string PLEASE_ENTER_WEB_SITE = "Please enter website!";
        public const string PLEASE_ENTER_CONTACT_PERSON = "Please enter conatct person!";
        public const string PLEASE_ENTER_SCREEN_NAME = "Please enter screen name!";
        public const string PLEASE_ENTER_CERTIFICATE = "Please enter certificate!";

        public const string PLEASE_CLICK_TO_SAVE = "Please click to Save!";
        public const string PLEASE_CLICK_TO_EDIT = "Please click to Edit!";
        public const string PLEASE_CLICK_TO_DELETE = "Please click to Delete!";
        public const string PLEASE_CLICK_TO_LOGIN = "Please click to Login!";
        public const string PLEASE_CLICK_TO_GET_PASSWORD = "Please click to get Password!";

        

        

    }
}
