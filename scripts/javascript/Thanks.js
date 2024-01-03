function ThanksViewModel()
{
    var self = this;
    this.BrandName = ko.observable("ShopShop")
    this.Role = ko.observable("Admin");
    this.UserName = ko.observable("");
    this.EmailId = ko.observable("");
    this.Status = ko.observable("");
    this.IsAdmin = ko.observable(false);
    var self = this;
    this.IsLoggedIn = ko.observable(false);
    this.CartItems = ko.observable(0);
    this.LoggedIn = ko.computed(function () { return self.UserName() != ""; });
    $.getJSON("/api/ShopShop/LoggedUserName", function (allData) {
        self.UserName(allData);
        if (self.UserName() != "") {
            self.IsLoggedIn(true);
            self.Status("Logout");
            self.IsAdmin(false);
            $.getJSON("/api/ShopShop/GetRole", function (Role) {
                if (Role == "Admin")
                    self.IsAdmin(true);
            });
            $.getJSON("/api/ShopShop/LoggedUserEmailId", function (allData) {
                self.EmailId(allData);
            });}
        else {
            self.IsLoggedIn(false);
            self.IsAdmin(false);
            self.Status("Login")
        }
    });
    this.Logout = function () {
        if (self.Status() == "Logout") {
            $.getJSON("/api/ShopShop/Logout", function (allData) {
                if (allData) {
                    alert("You are logged out");
                    window.open('Login.html', '_self');
                }
                else
                    alert("Unable to logout");
            });
        }
        else if (self.Status() == "Login") {
            window.open('Login.html', '_self')
        }
    }

    $.getJSON("/api/ShopShop/CartProductsCount", function (count) {
        self.CartItems(count);
    });
    var urlParams = new URLSearchParams(window.location.search);
    var values = urlParams.values();
    //this.OrderId=values[0];
   // alert(values[0])
    for(val of values)
    {
        self.OrderId = val;
    }
    // = ko.observable("3432-3235yyd-3yfdey89-3we567yt-d34r");
    this.EmailId = self.UserName;
}
ko.applyBindings(ThanksViewModel)