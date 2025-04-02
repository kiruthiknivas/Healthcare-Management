namespace HealthcareApi.DTOs
{
    public class PaymentDto
    {
        public int AppointmentID { get; set; }
        public double AmountPaid { get; set; }
        //public string PaymentToken { get; set; } // Token for payment processing
    }
}