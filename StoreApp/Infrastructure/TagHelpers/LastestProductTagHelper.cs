using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Entities.Models;

using Services.Contracts;

namespace StoreApp.Infrastructure.TagHelpers;

[HtmlTargetElement("div", Attributes="products")]
public class LatestProductTagHelper : TagHelper
{
    private readonly IServiceManager _manager;
    
    [HtmlAttributeName("number")]
    public int Number {get; set;}

    public LatestProductTagHelper(IServiceManager manager)
    {
        _manager = manager;
    }
    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        TagBuilder div = new TagBuilder("div");
        div.Attributes.Add("class","my-3");

        TagBuilder h6 = new TagBuilder("h6");
        h6.Attributes.Add("class","lead");

        TagBuilder icon = new TagBuilder("i");
        icon.Attributes.Add("class","fa fa-box text-secondary");
        h6.InnerHtml.AppendHtml(icon);
        h6.InnerHtml.AppendHtml("  Latest Products");

        TagBuilder ul = new TagBuilder("ul");
        var products= _manager.ProductService.GetLatestProducts(Number, false);
         foreach (Product p in products)
         {
            TagBuilder li = new TagBuilder("li");
            TagBuilder a = new TagBuilder("a");
            a.Attributes.Add("href", $"/product/get/{p.ProductId}");
            a.Attributes.Add("class", "text-primary");
            a.InnerHtml.AppendHtml(p.ProductName);
            li.InnerHtml.AppendHtml(a);
            ul.InnerHtml.AppendHtml(li);
         }

        div.InnerHtml.AppendHtml(h6);
        div.InnerHtml.AppendHtml(ul);
        output.Content.AppendHtml(div);

        
    }
}
