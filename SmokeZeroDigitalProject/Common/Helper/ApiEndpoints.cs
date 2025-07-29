﻿namespace SmokeZeroDigitalProject.Helpers
{
    public static class ApiEndpoints
    {
        // Auth
        public const string Login = "/api/Auth/login";
        public const string Register = "/api/Auth/register";
        public const string GoogleLogin = "/api/Auth/google-login";
        public const string ConfirmEmail = "/api/Auth/confirm-email";
        public const string ConfirmEmailByEmail = "/api/Auth/confirm-email-by-email";


        // Plan
        public const string GetAllPlan = "/api/PaymentPlan/all";
        public const string CreatePlan = "/api/PaymentPlan";
        public const string GetPlan = "/api/PaymentPlan";
        public const string CreatePaymentUrl = "/api/PaymentPlan/payment-url";
        public const string VNPayCallback = "/api/PaymentPlan/callback";


        // Coach
        public const string CreateCoach = "/api/Coach";
        public const string UpdateCoach = "/api/Coach/{id}";
        public const string GetCoachById = "/api/Coach/{id}";
        public const string GetAllCoach = "/api/Coach";

        // User
        public const string GetAllUser = "/api/User/all";
        public const string GetUserById = "/api/User/{userId}";
        public const string UpdateUser = "/api/User/update";
        public const string DeleteUser = "/api/User/delete";
        public const string GetUsersByPlanId = "/api/User/by-plan/{planId}";


        // Feedback
        public const string CreateFeedback = "/api/Feedback";
        public const string GetFeedbackById = "/api/Feedback/{id}";
        public const string GetAllFeedback = "/api/Feedback";
        public const string GetFeedbackByCoachId = "/api/Feedback/by-coach/{coachId}";

        // Comment
        public const string CreateComment = "/api/Comment";
        public const string UpdateComment = "/api/Comment";
        public const string DeleteComment = "/api/Comment/{id}";
        public const string GetCommentById = "/api/Comment/{id}";
        public const string GetAllComment = "/api/Comment";
        public const string GetCommentByPostId = "/api/Comment/by-post/{id}";
        public const string GetCommentByArticleId = "/api/Comment/by-article/{id}";
        public const string GetReplies = "/api/Comment/replies/{id}";

        //Blog
        public const string GetAllBlogs = "/api/Blog";
        public const string GetBlogById = "/api/Blog/by-id/{id}";
        public const string DeleteBlog = "/api/Blog/delete";

        // Chat
        public const string SendMessage = "/api/Chat/send";
        public const string GetMessages = "/api/Chat/messages/{conversationId}";
        public const string GetConversations = "/api/Chat/conversations";
        public const string GetMessageById = "/api/Chat/message/{messageId}";
        public const string GetOrCreateConversation = "/api/Chat/conversation";

        // Notification
        public const string GetAllNotis = "/api/Noti";
        public const string CreateNoti = "/api/Noti";


        // 🟢 Quitting Plan (Lập kế hoạch cai thuốc)
        public const string CreateQuittingPlan = "/api/QuittingPlan/create";
        public const string GetAllQuittingPlan = "/api/QuittingPlan/all";
        public const string GetQuittingPlanByUser = "/api/QuittingPlan/by-user/{userId}";

        // 🟠 Smoking Record (Ghi nhận trạng thái hút thuốc)
        public const string CreateSmokingRecord = "/api/SmokingRecord/create";
        public const string GetAllSmokingRecords = "/api/SmokingRecord/all";
        public const string GetSmokingRecordsByUser = "/api/SmokingRecord/by-user/{userId}";

        // 🔵 Progress Entry (Xem tiến trình)
        public const string CreateProgressEntry = "/api/ProgressEntry/create";
        public const string GetAllProgressEntries = "/api/ProgressEntry/all";
        public const string GetProgressEntriesByUser = "/api/ProgressEntry/by-user/{userId}";
    }

    public class ApiConfig
    {
        private readonly IConfiguration _configuration;

        public ApiConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string BaseUrl => _configuration["ApiSettings:BaseUrl"] ?? string.Empty;

        public string GetEndpoint(string endpoint) => $"{BaseUrl}{endpoint}";
    }
}
