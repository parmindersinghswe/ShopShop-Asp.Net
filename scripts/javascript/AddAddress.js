function AddressModel(obj)
{
    this.City = obj.City;
    this.LocalityOrStreet = obj.LocalityOrStreet;
    this.FlateNoBuilding = obj.FlateNoBuildingName;
    this.PinCode = obj.PinCode;
    this.State = obj.State;
    this.LandMark = obj.LandMark;
    this.FirstName = obj.FirstName;
    this.LastName = obj.LastName;
    this.MobileNumber = obj.MobileNumber;
    this.AlternateMobileNumber = obj.AlternateMobileNumber;
}
function AddAddressModel() {
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
    var urlParams = new URLSearchParams(window.location.search);
    var values = urlParams.values();
    this.id = "";
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

    this.Address = ko.observable(new AddressModel({ City: null, LocalityOrStreet: null, FlateNoBuildingName: null, PinCode: null, State: null, LandMark: null, FirstName: null, LastName:null, MobileNumber: null, AlternateMobileNumber: null }));
    this.AddAddress = function ()
    {
        alert(JSON.stringify(self.Address()));
        $.ajax('/api/ShopShop/PostAddress',{
        data:JSON.stringify(self.Address()),
        type: 'Post', contentType: 'application/json',
        success: function (result) {
            if (result)
            {

                alert("Address Added Successfully.");


                for(val of values)
                {
                    self.id = val;
                    alert("self.id")
                }
                if (self.id == "CartProducts")
                {
                    window.open('AddressesList.html','_Self')
                }
                else if (self.id == "")
                {
                    window.open('Home.html', '_Self');
                  
                }
                else
                    window.open('AddressesList.html?PId=' + self.id, '_Self')
                  
            }
            else
            {
                alert("Can't Proceed, something go went wrong");
            }
        }
    });
    }
    this.Later = function () { window.open('Home.html', '_Self');}
  
}

ko.applyBindings(AddAddressModel)
