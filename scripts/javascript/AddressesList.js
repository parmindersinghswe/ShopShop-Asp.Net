function AddressesListModel(Obj)
{
    this.Id = Obj.Id;
    this.UserId = Obj.UserId;
    this.City = Obj.City;
    this.LocalityOrStreet = Obj.LocalityOrStreet;
    this.FlatNoBuildingName = Obj.FlatNoBuildingName;
    this.PinCode = Obj.PinCode;
    this.State = Obj.State;
    this.LandMark = Obj.LandMark;
    this.Name = Obj.Name;
    this.MobileNumber = Obj.MobileNumber;
    this.AlternateMobileNumber = Obj.AlternateMobileNumber;
    this.IsSelected = Obj.IsSelected;
}
function AddressesListViewModel() {
    var self = this;
    this.BrandName = ko.observable("ShopShop")
    this.Role = ko.observable("Admin");
    this.UserName = ko.observable("");
    this.Status = ko.observable("");
    var self = this;
    this.id="";
    var urlParams = new URLSearchParams(window.location.search);
    var values = urlParams.values();
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
    
    self.AllAddresses = ko.observableArray();
    $.getJSON("/api/ShopShop/GetAllAddresses", function (AllAddresses) {
        var mappedAddresses = $.map(AllAddresses, function (address) { return new AddressesListModel(address); });
        self.AllAddresses(mappedAddresses);
       // alert(JSON.stringify(self.AllAddresses()));
    });
    this.SelectedAddressId = ko.observable("a56f8b81-cd82-4e91-9d5f-9cdb5aebc29a");
    this.AddNewAddress=function(add)
    {
       // alert("clicked value is: " + self.SelectedAddress())
        self.IsAdding(!self.IsAdding());
    }
    
    this.IsAdding = ko.observable(false);
    this.NewAddress = ko.observable("");
    this.AddNewAddress=function()
    {
       
                        for(val of values)
                        {
                            self.id = val;
                            alert("self.id")
                        }
              if(self.id!="")
                  window.open('AddAddress.html?PId=' + self.id, '_Self')
                        else
                  window.open('AddAddress.html?ProductsFrom=CartProducts','_Self')
    }
    //this.Add=function()
    //{
    //    $.ajax("/api/ShopShop/PostAddress", {
    //        data: ko.toJSON({ UserAddress: self.NewAddress() }),
    //        type: "post", contentType: "application/json",
    //        success: function (result) {
    //            if(result)
    //            {
    //                alert("New Address is added");
           
    //                $.getJSON("/api/ShopShop/GetAllAddresses", function (addresses) {
    //                    self.AllAddresses(addresses);
    //                });
    //                self.IsAdding(false);
    //                self.NewAddress("");
    //            }
    //        }
    //    });
    //}
 
    this.Next = function () {
      //  alert(self.SelectedAddressId());
        if(self.SelectedAddressId()=="")
            alert("Please Select An Address.")
        else
        {
           // alert("updateing........." + JSON.stringify({ UserAddress: self.SelectedAddressId() }));
            $.ajax("/api/ShopShop/PutSelectedAddress?AddressId="+self.SelectedAddressId(), {
              
                type: "put", contentType: "application/json",
                success: function (result) {
                    if (result)
                    {
                       
                        for(val of values)
                        {
                            self.id = val;
                        }
                        if (self.id != "")
                            window.open("Summary.html?ProductId=" + self.id, "_self")
                        else
                            window.open("Summary.html", "_Self");
                    }
                    else
                        alert("Unabl to select Address.")
                }
            });
        }
    }
    this.Remove = function (address) {
        alert(JSON.stringify(address));
        $.ajax("/api/ShopShop/DeleteAddress?id=" +address.Id, {

            type: "delete", contentType: "application/json",
            success: function (result) { self.AllAddresses.Remove(address);}
        });
    }
   
}

ko.applyBindings(AddressesListViewModel)
