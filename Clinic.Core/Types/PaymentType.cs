namespace Clinic.Core.Types
{
    public enum PaymentType
    {

        /// <summary>   پذیرش و پیش پرداخت </summary>
        PrePayment = 1,


        /// <summary>   کامل پرداخت شده </summary>
        FullPayment = 2,


        /// <summary>   وسایل تحویل خریدار شده و تسویه شده </summary>
        ClearPayment =3
    }
}