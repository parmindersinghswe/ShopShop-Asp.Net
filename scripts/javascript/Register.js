
function UserModel(User)
{
    this.FirstName = User.FirstName;
    this.LastName = User.LastName;
    this.Email = User.Email;
    this.MobileNumber = User.MobileNumber;
    this.Password = User.Password;
    this.ConfirmPassword = User.ConfirmPassword;
    this.SecurityQuestion = User.SecurityQuestion;
    this.SecurityAnswer = User.SecurityAnswer;
}
function RegistrationViewModel()
{
    var self = this;
    this.BrandName = ko.observable("ShopShop")
    //this.Role = ko.observable("Admin");
    //this.UserName = ko.observable("");
    this.Status = ko.observable("Login");
    var self = this;
    this.CartProducts=ko.observable(0);
    //$.getJSON("/api/ShopShop/LoggedUser", function (allData) {
    //    self.UserName(allData);
    //    if (self.UserName() != "")
    //        self.Status ("Logout")
    //    else
    //        self.Status ("Login")
    //});
   
    this.User = ko.observable(new UserModel({ FirstName: null, LastName: null, Email: null, MobileNumber: null, Password: null, ConfirmPassword: null, SecurityQuestion: null, SecurityAnswer: null }));
    this.NotNull=function()
    {
        if(self.User().FirstName==null || self.User().LastName==null || self.User().Email==null || self.User().MobileNumber==null || self.User().Password==null || self.User().SecurityQuestion==null || self.User().SecurityAnswer==null)
        {
            alert("Every field required");
            return false;
        }
        return true;
    }
    this.Register=function()
    {
        if (self.User().Password === self.User().ConfirmPassword && self.NotNull()) {
            $.ajax("/api/ShopShop/Registeration", {
                data: JSON.stringify(self.User()),
                type: "post", contentType: "application/json",
                success: function (result) {
                    if (result) {
                        alert("Registered with UserName: " + self.User().Email)
                        window.open("AddAddress.html", '_self');
                    }
                    else
                        alert("unable to register")
                }
            });
        }
        else
            console.log("Confirm password Error");
    }
    
    this.Login=function()
    {
        window.open('Login.html', '_self');
    }
    //this.Logout = function () {
    //    if (self.Status() == "Logout") {
    //        $.getJSON("/api/ShopShop/Logout", function (allData) {
    //            if (allData) {
    //                alert("You are logged out");
    //                window.open('Login.html', '_self');
    //            }
    //            else
    //                alert("Unable to logout");
    //        });
    //    }
    //    else if(self.Status()=="Login")
    //    {
    //        window.open('Login.html','_self')
    //    }
    //}
}

ko.applyBindings(RegistrationViewModel)
