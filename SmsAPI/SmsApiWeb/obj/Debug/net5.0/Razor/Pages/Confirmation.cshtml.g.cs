#pragma checksum "C:\Users\guial\Downloads\SmsAPI - Copy-20250410T192007Z-001\SmsAPI - Copy\SmsApiWeb\Pages\Confirmation.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "f812c506236078f7c79fde1de51f620816d8f511"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(SmsApiWeb.Pages.Pages_Confirmation), @"mvc.1.0.razor-page", @"/Pages/Confirmation.cshtml")]
namespace SmsApiWeb.Pages
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
#line 1 "C:\Users\guial\Downloads\SmsAPI - Copy-20250410T192007Z-001\SmsAPI - Copy\SmsApiWeb\Pages\_ViewImports.cshtml"
using SmsApiWeb;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"f812c506236078f7c79fde1de51f620816d8f511", @"/Pages/Confirmation.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"b66e4efa1051241fd743451ab2f719aa663efb22", @"/Pages/_ViewImports.cshtml")]
    public class Pages_Confirmation : global::Microsoft.AspNetCore.Mvc.RazorPages.Page
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 3 "C:\Users\guial\Downloads\SmsAPI - Copy-20250410T192007Z-001\SmsAPI - Copy\SmsApiWeb\Pages\Confirmation.cshtml"
  
    ViewData["Title"] = "Home page";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<div class=\"container contact-container\">\r\n    <h1 class=\"mt-4 mb-3\">\r\n        Confirmation\r\n    </h1>\r\n    <hr />\r\n");
#nullable restore
#line 12 "C:\Users\guial\Downloads\SmsAPI - Copy-20250410T192007Z-001\SmsAPI - Copy\SmsApiWeb\Pages\Confirmation.cshtml"
     if (!string.IsNullOrEmpty(Model.Message))
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <h3>");
#nullable restore
#line 14 "C:\Users\guial\Downloads\SmsAPI - Copy-20250410T192007Z-001\SmsAPI - Copy\SmsApiWeb\Pages\Confirmation.cshtml"
       Write(Model.Message);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h3>\r\n");
#nullable restore
#line 15 "C:\Users\guial\Downloads\SmsAPI - Copy-20250410T192007Z-001\SmsAPI - Copy\SmsApiWeb\Pages\Confirmation.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("    <br />\r\n</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<SmsApiWeb.Pages.ConfirmationModel> Html { get; private set; }
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<SmsApiWeb.Pages.ConfirmationModel> ViewData => (global::Microsoft.AspNetCore.Mvc.ViewFeatures.ViewDataDictionary<SmsApiWeb.Pages.ConfirmationModel>)PageContext?.ViewData;
        public SmsApiWeb.Pages.ConfirmationModel Model => ViewData.Model;
    }
}
#pragma warning restore 1591
