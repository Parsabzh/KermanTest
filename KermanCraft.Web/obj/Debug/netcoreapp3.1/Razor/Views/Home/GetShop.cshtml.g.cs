#pragma checksum "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c87a65820ef537e899cdbb9ecd0907bcad2db3c0"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_GetShop), @"mvc.1.0.view", @"/Views/Home/GetShop.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\_ViewImports.cshtml"
using KermanCraft.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\_ViewImports.cshtml"
using KermanCraft.Domain.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"c87a65820ef537e899cdbb9ecd0907bcad2db3c0", @"/Views/Home/GetShop.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"06fa53d569a72b552a97dfc6bcdc86f0666136e5", @"/Views/_ViewImports.cshtml")]
    public class Views_Home_GetShop : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<KermanCraft.Application.ViewModels.Product.ProductTypeViewModel>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/front/Content/css/cardrozane .css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("text-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "GetShopByType", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml"
  
    ViewData["Title"] = "GetShop";
    var n = 0;

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "c87a65820ef537e899cdbb9ecd0907bcad2db3c05159", async() => {
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

<br />
<br />
<br />
<h1 class=""text-white  heading-1""><span>فروشگاه</span></h1>

<main class=""site-main post-238 envato_tk_templates type-envato_tk_templates status-publish has-post-thumbnail hentry"" role=""main"">
    <div class=""page-content"">
        <div data-elementor-type=""wp-post"" data-elementor-id=""238"" class=""elementor elementor-238"" data-elementor-settings=""[]"">
            <div class=""elementor-inner"">
                <div class=""elementor-section-wrap"">
                    <section class=""elementor-element elementor-element-2fa4a70 elementor-section-boxed elementor-section-height-default elementor-section-height-default elementor-section elementor-top-section"" data-id=""2fa4a70"" data-element_type=""section"">
                        <div class=""elementor-container elementor-column-gap-default"">
                            <div class=""elementor-row"">
");
#nullable restore
#line 22 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml"
                                   var count = @Model.Count();

#line default
#line hidden
#nullable disable
#nullable restore
#line 23 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml"
                                 for (var i = 0; i < 2; i++)
                                {
                                    if (count == 0)
                                    {
                                        break;
                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                    <div class=""elementor-element elementor-element-bbf2aa5 elementor-column elementor-col-50 elementor-top-column"" data-id=""bbf2aa5"" data-element_type=""column"">
                                        <div class=""elementor-column-wrap  elementor-element-populated"">
                                            <div class=""elementor-widget-wrap"">


");
#nullable restore
#line 34 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml"
                                                 for (var j = 0; j < 2; j++)
                                                {
                                                    if (count == 0)
                                                    {
                                                        break;
                                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                    <section class=""elementor-element elementor-element-c4e2d73 elementor-section-boxed elementor-section-height-default elementor-section-height-default elementor-section elementor-inner-section"" data-id=""c4e2d73"" data-element_type=""section"">
                                                        <div class=""elementor-container elementor-column-gap-default"">
                                                            <div class=""elementor-row"">
");
#nullable restore
#line 43 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml"
                                                                 for (; n < 2; n++)
                                                                {
                                                                    if (count == 0)
                                                                    {
                                                                        break;
                                                                    }
                                                                    count--;
                                                                    if ((n % 2) == 0 && n != 0)
                                                                    {
                                                                        break;
                                                                    }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"                                                                    <div class=""elementor-element elementor-element-4d2e005 elementor-column elementor-col-50 elementor-inner-column"" data-id=""4d2e005"" data-element_type=""column"">
                                                                        <div class=""elementor-column-wrap  elementor-element-populated"">
                                                                            <div class=""elementor-widget-wrap"">
                                                                                <div class=""elementor-element elementor-element-497e123 elementor-cta--skin-cover elementor-cta--valign-middle elementor-bg-transform elementor-bg-transform-zoom-out elementor-animated-content elementor-widget elementor-widget-call-to-action"" data-id=""497e123"" data-element_type=""widget"" data-widget_type=""call-to-action.default"">
                                                                                    <div class=""elementor-widget-container"">
      ");
            WriteLiteral(@"                                                                                  <div class=""elementor-cta"">
                                                                                            <div class=""elementor-cta__bg-wrapper"">
                                                                                                <div class=""elementor-cta__bg elementor-bg""");
            BeginWriteAttribute("style", " style=\"", 4958, "\"", 5029, 4);
            WriteAttributeValue("", 4966, "background-image:", 4966, 17, true);
            WriteAttributeValue(" ", 4983, "url(/images/ProductType/", 4984, 25, true);
#nullable restore
#line 61 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml"
WriteAttributeValue("", 5008, Model[0].ImageName, 5008, 19, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 5027, ");", 5027, 2, true);
            EndWriteAttribute();
            WriteLiteral(@"></div>
                                                                                                <div class=""elementor-cta__bg-overlay""></div>
                                                                                            </div>
                                                                                            <div class=""elementor-cta__content"">

                                                                                                ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "c87a65820ef537e899cdbb9ecd0907bcad2db3c012905", async() => {
                WriteLiteral(@"
                                                                                                    <h3 class=""elementor-cta__title elementor-cta__content-item elementor-content-item elementor-animated-item--grow"">

                                                                                                        ");
#nullable restore
#line 69 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml"
                                                                                                   Write(Model[n].Name);

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n\r\n                                                                                                    </h3>\r\n                                                                                                ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_3.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_4.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-type", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 66 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml"
                                                                                                                                                                           WriteLiteral(Model[n].Name);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["type"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-type", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["type"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"

                                                                                            </div>
                                                                                        </div>
                                                                                    </div>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
");
#nullable restore
#line 81 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml"

                                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                                            </div>\r\n                                                        </div>\r\n                                                    </section>\r\n");
#nullable restore
#line 86 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml"

                                                }

#line default
#line hidden
#nullable disable
            WriteLiteral("                                            </div>\r\n                                        </div>\r\n                                    </div>\r\n");
#nullable restore
#line 91 "C:\KermanCraft\KermanCraft\KermanCraft.Web\Views\Home\GetShop.cshtml"
                                }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                            </div>
                        </div>
                    </section>
                </div>
            </div>
        </div>
        <div class=""post-tags"">
        </div>
    </div>

    <section id=""comments"" class=""comments-area"">
    </section>
</main>
<br />
<br />");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<KermanCraft.Application.ViewModels.Product.ProductTypeViewModel>> Html { get; private set; }
    }
}
#pragma warning restore 1591
