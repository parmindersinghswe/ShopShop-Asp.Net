function LoginModel(User) {
    this.UserName = User.UserName;
    this.Password = User.Password;

}
function LoginViewModel() {

    this.BrandName = ko.observable("ShopShop")
    var self = this
    this.UserName = ko.observable("parmisingh12@gmail.com");
    this.Password = ko.observable("parminder.");
    var self = this;
    this.loggedin = false;
    this.User = ko.computed(function () { return new LoginModel({ UserName: self.UserName(), Password: self.Password() }); });
    this.Login = function () {
        $.ajax("/api/ShopShop/Login", {
            data: JSON.stringify(self.User()),
            type: "post", contentType: "application/json",
            success: function (result) {
                if (result) {
                    window.open('Home.html', '_self');
                }
                else {
                    alert("Wrong: UserName Or Password");
                }
            }
        });
    }
    this.Register = function () {
        window.open('Register.html', '_self');
    }
}
ko.applyBindings(LoginViewModel);