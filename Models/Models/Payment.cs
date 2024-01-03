using AuthorizeNet.Api.Contracts.V1;
using System;
using System.Collections.Generic;
using System.Web.Security;
using System.Linq;
using ShopShop.Models.DataBaseModels;

namespace ShopShop.Models.Models
{
    public class Payment
    {
        public static ANetApiResponse ChargeCreditCard(String ApiLoginID, String ApiTransactionKey, decimal amount, CreditCard Card, BillingAddress BillingAddress, List<lineItemType> ItemsList)
        {
            AuthorizeNet.Api.Controllers.Bases.ApiOperationBase<ANetApiRequest, ANetApiResponse>.RunEnvironment = AuthorizeNet.Environment.SANDBOX;
            AuthorizeNet.Api.Controllers.Bases.ApiOperationBase<ANetApiRequest, ANetApiResponse>.MerchantAuthentication = new merchantAuthenticationType()
            {
                name = ApiLoginID,
                ItemElementName = ItemChoiceType.transactionKey,
                Item = ApiTransactionKey,
            };
            var creditCard = new creditCardType
            {
                cardNumber = Card.CardNumber,
                expirationDate = ((Card.ExpirationDate.Month.ToString()) + Card.ExpirationDate.Year.ToString()),
                cardCode = Card.CardCode.ToString()
            };

            var billingAddress = new customerAddressType
            {
                firstName = BillingAddress.firstName,
                lastName = BillingAddress.lastName,
                address = BillingAddress.address,
                city = BillingAddress.city,
                zip = BillingAddress.zip
            };
            var paymentType = new paymentType { Item = creditCard };
            var lineItems = ItemsList;
            var transactionRequest = new transactionRequestType
            {
                transactionType = transactionTypeEnum.authCaptureTransaction.ToString(),    // charge the card
                amount = amount,
                payment = paymentType,
                billTo = billingAddress,
                lineItems = lineItems.ToArray()
            };

            var request = new createTransactionRequest { transactionRequest = transactionRequest };
            var controller = new AuthorizeNet.Api.Controllers.createTransactionController(request);
            controller.Execute();
            var response = controller.GetApiResponse();
          //  var id = response.transactionResponse.transId;
            if (response != null)
            {
                
                if (response.messages.resultCode == messageTypeEnum.Ok)
                {
                    if (response.transactionResponse.messages != null)
                    {
                        Console.WriteLine("Successfully created transaction with Transaction ID: " + response.transactionResponse.transId);
                        Console.WriteLine("Response Code: " + response.transactionResponse.responseCode);
                        Console.WriteLine("Message Code: " + response.transactionResponse.messages[0].code);
                        Console.WriteLine("Description: " + response.transactionResponse.messages[0].description);
                        Console.WriteLine("Success, Auth Code : " + response.transactionResponse.authCode);
                    }
                    else
                    {
                        Console.WriteLine("Failed Transaction.");
                        if (response.transactionResponse.errors != null)
                        {
                            Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                            Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Failed Transaction.");
                    if (response.transactionResponse != null && response.transactionResponse.errors != null)
                    {
                        Console.WriteLine("Error Code: " + response.transactionResponse.errors[0].errorCode);
                        Console.WriteLine("Error message: " + response.transactionResponse.errors[0].errorText);
                    }
                    else
                    {
                        Console.WriteLine("Error Code: " + response.messages.message[0].code);
                        Console.WriteLine("Error message: " + response.messages.message[0].text);
                    }
                }
            }
            else
            {
                Console.WriteLine("Null Response.");
            }

            return response;
        }
        public static bool Pay(CreditCard Card, Guid ProductId, int Qty)
        {
            Guid NullId = new Guid("00000000-0000-0000-0000-000000000000");
            BillingAddress billingAddress;
            Guid UserId = Guid.Parse(Membership.GetUser().ProviderUserKey.ToString());
            List<lineItemType> ItemsList;
            decimal TotalAmount = 0;
            if (ProductId == NullId)
            {
                using (var context = new ShoppingCartEntities())
                {
                    var Cart = (from cart in context.Carts.Include("CartProductsLists.Product") where (cart.UserId == UserId) select cart).ToList().FirstOrDefault();
                    ItemsList = (from ProductList in Cart.CartProductsLists
                                 select (new lineItemType()
                                 {
                                     itemId = ProductList.Product.FriendlyId.ToString(),
                                     name = ProductList.Product.Name.ToString(),
                                     quantity = ProductList.Qty,
                                     unitPrice = ProductList.Product.Price
                                 })).ToList();
                }
                foreach (var item in ItemsList)
                    TotalAmount += item.unitPrice * item.quantity;
            }
            else
            {
                using (var context = new ShoppingCartEntities())
                {
                    ItemsList = (from products in context.Products
                                 where (products.Id == ProductId)
                                 select (new lineItemType()
                                 {
                                     itemId = products.FriendlyId.ToString(),
                                     name = products.Name.ToString(),
                                     quantity = Qty,
                                     unitPrice = products.Price
                                 })).ToList();

                }
                TotalAmount = ItemsList[0].unitPrice * Qty;
            }
            using (var context = new ShoppingCartEntities())
            {
                var User = (from users in context.Users where (users.Id == UserId) select users).ToList().FirstOrDefault();
                var addres = context.Addresses.ToList();
                var Address = context.BillingAddresses.ToList().FirstOrDefault();
                billingAddress = new BillingAddress()
                {
                   firstName=Address.FirstName,
                   lastName=Address.LastName,
                   address=Address.FlatNoBuildingName+Address.LocalityOrStreet,
                   city=Address.City,
                   zip=Address.PinCode
                };
            }

            var response = ChargeCreditCard("", "", TotalAmount, Card, billingAddress, ItemsList);

            return response.messages.resultCode.ToString().Equals("Ok");
        }
    }
}