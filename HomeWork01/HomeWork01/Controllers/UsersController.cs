using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork01.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet]
        public ActionResult<List<string>> Get()
        {
            return Ok(UserNames.UserFullNames);
        }

        [HttpGet("{index}")]
        public ActionResult<string> GetUserByIndex(int index)
        {
            try
            {
                if (index<0)
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "Uff... do you feel that cold? That's because you are below zero temperature!");
                }

                if (index>=UserNames.UserFullNames.Count)
                {
                    return StatusCode(StatusCodes.Status404NotFound, "Alright people move along. Nothing to see here...");
                }

                return Ok(UserNames.UserFullNames[index]);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred. Who you gonna call? Ok I'll stop now");                
            }
        }

        [HttpPost]
        public IActionResult AddNewUser()
        {
            try
            {
                using (StreamReader reader = new StreamReader(Request.Body))
                {
                    string newUser = reader.ReadToEnd();

                    if (string.IsNullOrEmpty(newUser))
                    {
                        return StatusCode(StatusCodes.Status400BadRequest, "Please don't leave empty field, and enter full name of user.");
                    }

                    UserNames.UserFullNames.Add(newUser);
                    return StatusCode(StatusCodes.Status200OK, "New user was successfully added.");
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error has occurred. Please contact the administrator.");              
            }
        }
    }
}
