function DateModel(date)
{
    this.Month = date.Month;
    this.Year = date.Year;
}
function CreditCardModel(obj)
{
    this.CardNumber = obj.CardNumber;
    this.ExpirationDate=(new DateModel(obj.ExpirationDate));
    this.CardCode = obj.CardCode;
    this.CardHolderName = obj.CardHolderName;
}
function PurchaseModel(obj)
{
    this.CreditCard = new CreditCardModel(obj.CreditCard);
    this.ProductId = obj.ProductId;
    this.Qty = obj.Qty;
}
function PaymentViewModel() {
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


    this.Months = ["MONTH",1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12];
    this.Years=["YEAR",2018,2019,2020,2021,2022,2023]
    this.Purchase = ko.observable(new PurchaseModel({ CreditCard: { CardNumber: null, ExpirationDate: { Month: null, Year: null }, CardCode: null, CardHolderName: null },ProductId:null,Qty:null}))
    var urlParams = new URLSearchParams(window.location.search);
    var values = urlParams.values();
    this.id = ko.computed(
    function () {
        var Id = [];
        i = 0;
        for(val of values)
        {
            Id[i] = val;
            i++;
        }
        return Id;
    }
    );
    this.Submit = function ()
    {
        self.Purchase().ProductId = self.id()[0];
        self.Purchase().Qty = self.id()[1];
        $.ajax("/api/ShopShop/PostPayment",
            {
                data: JSON.stringify(self.Purchase()),
                type: 'Post', contentType: 'application/json',
                success: function (result)
                {
                    if (result!="00000000-0000-0000-0000-000000000000")
                        {
                        alert("Order Placed Successfully");
                        window.open('Thanks.html?OrderId='+result,'_Self')
                        }
                    else
                        alert("Unable to Place Ordser. Please try again....")
                }
            });
      
    }
}

ko.applyBindings(PaymentViewModel)
