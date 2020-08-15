using System.Linq;

namespace Clinic.FrameWork.Json
{
    public static class JsonExtensions
    {
        #region ToJson
        public static string StringArrayToJson(this string[] array)
        {
            if (array == null || !array.Any())
                return "[]";
            else
                return Newtonsoft.Json.JsonConvert.SerializeObject(array);
        }
        #endregion
    }
}
