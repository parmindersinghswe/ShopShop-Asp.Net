function BillingAddressModel(obj) {
    this.City = obj.City;
    this.LocalityOrStreet = obj.LocalityOrStreet;
    this.FlatNoBuildingName = obj.FlatNoBuildingName;
    this.PinCode = obj.PinCode;
    this.State = obj.State;
    this.LandMark = obj.LandMark;
    this.Name = obj.Name;
    this.MobileNumber = obj.MobileNumber;
}
function ProductModel(product) {
    this.Id = product.Id;
    this.CategoryId = product.CategoryId;
    this.Name = product.Name;
    this.Price = product.Price;
    this.TotalNumberOfProducts = product.TotalNumberOfProducts;
    this.Qty = ko.observable(product.Qty);
    this.TotalAmount = ko.computed(function () { return Number(this.Price) * Number(this.Qty()) }, this);
}
function CartProductListModel(obj) {
    this.ProductId = obj.ProductId;
    this.Qty = obj.Qty;
}
function SummaryViewModel() {
    var self = this;
    this.BrandName = ko.observable("ShopShop")
    this.Role = ko.observable("Admin");
    this.UserName = ko.observable("");
    //   this.UserId=ko.observable("");
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

    this.Address = ko.observableArray(null);
    var urlParams = new URLSearchParams(window.location.search);
    var values = urlParams.values();
    this.id = ko.computed(
        function()
        {
            var Id;
            for(val of values)
            {
                Id = val;
            }
            return Id;
        }
        );

    this.Products = ko.observableArray(null);
    this.ProductsCount = ko.observable(0);
    this.DeliveryPrice = ko.observable(10);
    this.BillingShippingSame = ko.observable(true);
    this.BillingAddress = ko.observable(new BillingAddressModel({ City: null, LocalityOrStreet: null, FlatNoBuildingName: null, PinCode: null, State: null, LandMark: null, Name: null, MobileNumber: null }));
    this.ShowAddress = ko.computed(function () { return !self.BillingShippingSame(); });
 ko.computed(
        function ()
        {
          
            if(self.id()==undefined)
            {
               
                $.getJSON("/api/ShopShop/GetCartProducts", function (allData) {

                    var mappedProducts = $.map(allData, function (item) { return new ProductModel(item); });
                    self.Products(mappedProducts);
                    self.ProductsCount(mappedProducts.length);

                });
            }
            else
            {
                
                $.getJSON("/api/ShopShop/GetProduct?ProductId=" + self.id(), function (product)
                {
                    var mappedProducts = $.map(product, function (item) { return new ProductModel(item); });
                    self.Products(mappedProducts);
                });
            }
            if(self.BillingShippingSame())
            {
                $("#ProductsHeader").css("margin-top", "25px");
            }
            else
            {
                $("#ProductsHeader").css("margin-top", "50px");
            }
        }
        );
  
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

    this.Bill = ko.computed(function () {

        var total = 0;
        self.Products().forEach(function (product) {total = Number(total) + Number(product.TotalAmount()); });
        QtyToDatabase();
        return total;
    });
    this.TotalBill = ko.computed(function () {
        return Number(self.Bill())+Number(self.DeliveryPrice());
    });

    $.getJSON('/api/ShopShop/GetSelectedAddress/', function (address) {
        self.Address(address);
        self.BillingAddress(address);
    });

    this.PayOnDelivery=function()
    {   alert("Pay On Delivery Not Available.")
    }
    this.PayNow = function () {
        $.ajax('/api/ShopShop/PostBillingAddress',
            {
                data: JSON.stringify(self.BillingAddress()),
                type: 'Post', contentType: 'application/json',
                success: function (result) { }
            });

        for(val of values)
        {
            self.id = val;
        }
        if (self.id() == undefined)
            window.open('Payment.html', '_Self')
        else {
           
            window.open('Payment.html?Pid=' + self.id() + '&Qty=' + self.Products()[0].Qty(), '_Self')
        }
        
    }
    this.ChangeOrAddAddress = function () {
     
        for(val of values)
        {
            self.id = val;
        }
        if (self.id()!=undefined)
            window.open("AddressesList.html?ProductId=" + self.id(), "_self");
        else
            window.open("AddressesList.html", '_Self');
    }

}

ko.applyBindings(SummaryViewModel)
