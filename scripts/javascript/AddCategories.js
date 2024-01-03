function AddCategoriesViewModel() {
    var self = this;
    this.BrandName = ko.observable("ShopShop")
    this.Role = ko.observable("Admin");
    this.UserName = ko.observable("");
    this.Status = ko.observable("");
    this.Category = ko.observable("");
    this.Categories = ko.observableArray("")
    this.CartItems = ko.observable(0);
    var self = this;
    this.IsLoggedIn = ko.computed(function () { return self.UserName() != ""; });
    $.getJSON("/api/ShopShop/LoggedUserName", function (allData) {
        self.UserName(allData);
        if (self.UserName() != "")
            self.Status("Logout")
        else
            self.Status("Login")
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
    $.getJSON("/api/ShopShop/GetCategories", function (allData) { self.Categories(allData); });
    $.getJSON("/api/ShopShop/CartProductsCount", function (count) {
        self.CartItems(count);
    });
    this.Add = function () {
        this.WillAdd = true;
        Intern_self = this;
        self.Categories().forEach(function (element) {
            if (element == self.Category())
            { Intern_self.WillAdd = false; }
        })
        if (this.WillAdd) {
            $.ajax("/api/ShopShop/PostCategories", {
                data: ko.toJSON({ Name: self.Category() }),
                type: "post", contentType: "application/json",
                success: function (result) {
                    if (result) {
                        self.Category("");
                        alert("Category Is Added")
                    }
                    else
                        alert("Unable to Add Category")
                }
            });
        }
        else
            alert("Typed Category Is Already Registered");
        $.getJSON("/api/ShopShop/GetCategories", function (allData) { self.Categories(allData); });
    }
}
ko.applyBindings(AddCategoriesViewModel)
