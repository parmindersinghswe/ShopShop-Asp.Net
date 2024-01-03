function CategoryModel(category) {
    this.Id = category.Id;
    this.Name = category.Name;
    this.Products = $.map(category.Products, function (item) { return new ProductModel(item) });
}
function ProductModel(product) {
    this.Id = product.Id;
    this.CategoryId = product.CategoryId;
    this.Name = product.Name;
    this.Price = product.Price;
    this.TotalNumberOfProducts = product.TotalNumberOfProducts;
}

function HomeViewModel() {
    var self = this;
    this.BrandName = ko.observable("ShopShop")
    this.Role = ko.observable("Admin");
    this.UserName = ko.observable("");
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
        }
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

    this.Products = ko.observableArray(null);
    this.Categories = ko.observableArray(null);
    $.getJSON("/api/ShopShop/GetCategoriesWithProducts", function (allData) {

        var mappedTasks = $.map(allData, function (item) { return new CategoryModel(item) });
        self.Categories(mappedTasks);

    });
    this.Buy = function (product) {
        if (self.UserName() != "") {
            window.open("Summary.html?PId=" + product.Id, "_self")
        }
        else
            alert("First Login Please");
    }
    this.AddToCart = function (product) {

        if (self.UserName() != "") {
            $.ajax("/api/ShopShop/PostProductToCart", {
                data: ko.toJSON({ ProductId: product.Id, Qty: 1 }),
                type: "post", contentType: "application/json",
                success: function (result) {
                    if (result) {
                        self.CartItems(Number(self.CartItems()) + 1);
                      
                    }
                    else {
                        alert("Something went wrong! Try again....")
                    }
                }
            });
        }
        else
            alert("First Login");
    }
}

ko.applyBindings(HomeViewModel)
