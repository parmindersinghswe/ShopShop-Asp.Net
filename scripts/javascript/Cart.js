

function ProductModel(product) {
    this.Id = product.Id;
    this.CategoryId = product.CategoryId;
    this.Name = product.Name;
    this.Price = product.Price;
    this.TotalNumberOfProducts = product.TotalNumberOfProducts;
    this.Qty = ko.observable(product.Qty);
    this.TotalAmount = ko.computed(function () { return Number(this.Price) * Number(this.Qty()) },this);
}
function CartProductListModel(obj)
{
    this.ProductId = obj.ProductId;
    this.Qty = obj.Qty;
}
function CartViewModel() {
    var self = this;
    this.BrandName = ko.observable("ShopShop")
    this.Role = ko.observable("Admin");
    this.UserName = ko.observable("");
    this.Status = ko.observable("");
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

    this.Products = ko.observableArray(null);
    $.getJSON("/api/ShopShop/GetCartProducts", function (allData) {

        var mappedTasks = $.map(allData, function (item) { return new ProductModel(item); });
        self.Products(mappedTasks);
      
    });

    this.Buy = function (product)
    {
        if (self.UserName() != "") {
            window.open("BuyAddress.html?PId=" + product.Id, "_self")
        }
        else
            alert("First Login Please");
    }
    this.Remove = function (product)
    {
        if (confirm("Dou you realy want to delete User '" +product.Name + "'?")) {
            self.Products.remove(product);
            $.ajax("/api/ShopShop/DeleteProductFromCart?Id=" + product.Id,{
                type: "delete", contentType: "application/json",
                success: function (result) { if (result) alert("Successfully Removed");}
            });
        }
    }
    this.CartProductsList = ko.observable(new CartProductListModel({ ProdcutId: null, Qty: null }));

    this.QtyToDatabase = function () {
        var list = [];
        self.Products().forEach(
            function (item) {
                list.push(new CartProductListModel({ ProductId: item.Id, Qty: item.Qty() }))
               
            }
            );
        self.CartProductsList(list);
      
        $.ajax('/api/ShopShop/PutQuentity',
            {
                data: JSON.stringify(self.CartProductsList()),
                type: 'Put', contentType: 'application/json',
                success: function (result) { }
            });
    }
  
    this.TotalBill = ko.computed(function () {
       
        var total=0;
        self.Products().forEach(function (product) { total = Number(total) + Number(product.TotalAmount()); });
        QtyToDatabase();
        return total;
    });
   
    this.Continue=function()
    {
       window.open('Summary.html','_Self')
    }
    
}

ko.applyBindings(CartViewModel)
