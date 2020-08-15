using System.Text;

namespace Clinic.Core.Helpers
{
    public static class RandomText
    {
        #region Public Methods

        public static string Generate(string chars, int count)
        {
            var lenght = RandomNumberHelper.Next(count, count);
            var output = new StringBuilder(lenght);

            for (var i = 0; i < lenght; i++)
            {
                var randomIndex = RandomNumberHelper.Next(chars.Length - 1);
                output.Append(chars[randomIndex]);
            }

            return output.ToString();
        }

        #endregion Public Methods
    }
}