namespace SharedKernel.Resources;

public sealed class GeneralMessagesKeys
{
    private const string Main = nameof(GeneralMessageResource) + ":";


#pragma warning disable CA1034
    public static class Error
    {
        public const string NotFound = Main + nameof(GeneralMessageResource.Error_NotFound);
    }

#pragma warning disable CA1711
    public static class Exception
#pragma warning restore CA1711
    {
        public const string Unexpected = Main + nameof(GeneralMessageResource.Exception_Unexpected);
        public const string UnexpectedCreationF = Main + nameof(GeneralMessageResource.Exception_Unexpected_Creation_F);
        public const string UnexpectedCreationM = Main + nameof(GeneralMessageResource.Exception_Unexpected_Creation_M);
        public const string UnexpectedDeletingF = Main + nameof(GeneralMessageResource.Exception_Unexpected_Deleting_F);
        public const string UnexpectedDeletingM = Main + nameof(GeneralMessageResource.Exception_Unexpected_Deleting_M);
        public const string UnexpectedUpdatingF = Main + nameof(GeneralMessageResource.Exception_Unexpected_Updating_F);
        public const string UnexpectedUpdatingM = Main + nameof(GeneralMessageResource.Exception_Unexpected_Updating_M);
    }

    public static class Guard
    {
        public const string AlreadyExists = Main + nameof(GeneralMessageResource.Guard_AlreadyExists);
        public const string InvalidFormat = Main + nameof(GeneralMessageResource.Guard_InvalidFormat);
        public const string MaximumLenght = Main + nameof(GeneralMessageResource.Guard_MaximumLenght);
        public const string MaximumValue = Main + nameof(GeneralMessageResource.Guard_MaximumValue);
        public const string MinimumLenght = Main + nameof(GeneralMessageResource.Guard_MinimumLenght);
        public const string MinimumValue = Main + nameof(GeneralMessageResource.Guard_MinimumValue);
        public const string NotNullOrEmpty = Main + nameof(GeneralMessageResource.Guard_NotNullOrEmpty);
    }

    public static class Success
    {
        public const string CreationF = Main + nameof(GeneralMessageResource.Success_Creation_F);
        public const string CreationM = Main + nameof(GeneralMessageResource.Success_Creation_M);
        public const string DeletingF = Main + nameof(GeneralMessageResource.Success_Deleting_F);
        public const string DeletingM = Main + nameof(GeneralMessageResource.Success_Deleting_M);
        public const string General = Main + nameof(GeneralMessageResource.Success_General);
        public const string UpdatingM = Main + nameof(GeneralMessageResource.Success_Updating_M);
        public const string UpdatingF = Main + nameof(GeneralMessageResource.Success_Updating_F);
    }

    public static class Middleware
    {
        public static string ReturnExceptionTitle => $"{Main}.{nameof(GeneralMessageResource.Middleware_ReturnException_Title)}";
    }
#pragma warning restore CA1034
}
