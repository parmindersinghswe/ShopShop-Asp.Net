function ProductModel(obj)
{
    this.Id = obj.Id;
    this.CategoryId = obj.CategoryId;
    this.Name = obj.Name;
    this.Price = obj.Price;
    this.TotalNumberOfProducts = obj.TotalNumberOfProducts;
}
function AddProductsViewModel() {
    var self = this;
    this.BrandName = ko.observable("ShopShop")
    this.Role = ko.observable("Admin");
    this.UserName = ko.observable("");
    this.Status = ko.observable("");
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
    $.getJSON("/api/ShopShop/CartProductsCount", function (count) {
        self.CartItems(count);
    });
    var self = this;
    this.Category = ko.observable();
    this.Categories = ko.observableArray("");
    this.Name = ko.observable("");
    this.Price = ko.observable("");
    this.Count = ko.observable("");
    this.Product = ko.computed(function () { return new ProductModel({ Id: "", CategoryId: "", Name: self.Name(), Price: self.Price(), TotalNumberOfProducts: self.Count() }) });
    $.getJSON("/api/ShopShop/GetCategories", function (allData) { self.Categories(allData); });
    this.Add=function()
    {
        $.getJSON("/api/ShopShop/GetCategoryId?Category=" + self.Category(), function (CategoryId)
        {
            self.Product().CategoryId = CategoryId;
            alert(ko.toJSON(self.Product()));
            $.ajax("/api/ShopShop/PostProducts", {
                data: ko.toJSON(self.Product()),
                type: "post", contentType: "application/json",
                success: function (result) {
                    if (result) {
                        alert("Product Added")
                        self.Count("");
                        self.Name("");
                        self.Price("");
                    }
                    else
                        alert("Unable to add product")
                }
            });
        });   
    }
    this.Cancel=function()
    {
        alert(JSON.stringify(self.Product()))
       
    }
}

ko.applyBindings(AddProductsViewModel)
