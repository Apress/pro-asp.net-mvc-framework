using System;
using System.Web.Mvc;
using DomainModel.Entities;

namespace WebUI
{
public class CartModelBinder : IModelBinder
{
    private const string cartSessionKey = "_cart";

    public object BindModel(ControllerContext controllerContext, 
                            ModelBindingContext bindingContext)
    {
        // Some modelbinders can update properties on existing model instances. This
        // one doesn't need to - it's only used to supply action method parameters.
        if(bindingContext.Model != null)
            throw new InvalidOperationException("Cannot update instances");

        // Return the cart from Session[] (creating it first if necessary)
        Cart cart = (Cart)controllerContext.HttpContext.Session[cartSessionKey];
        if(cart == null) {
            cart = new Cart();
            controllerContext.HttpContext.Session[cartSessionKey] = cart;
        }
        return cart;
    }
}
}