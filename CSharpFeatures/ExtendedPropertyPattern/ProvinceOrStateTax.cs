using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CSharpFeatures.ExtendedPropertyPattern;
public class ProvinceOrStateTax
{
    public string ProvinceOrState { get; set; }
}

public class CalculateTax
{
    public string CountryName { get; set; }
    public ProvinceOrStateTax ProvinceTaxProperty { get; set; }
}

public class Computing
{
    private  static decimal ComputePrice(CalculateTax calculate, decimal price) =>
        calculate switch
        {
            { ProvinceTaxProperty.ProvinceOrState: "Québec" } => 1.148M * price,
            { ProvinceTaxProperty.ProvinceOrState: "Alberta" } => 1.05M * price,
            { ProvinceTaxProperty.ProvinceOrState: "Ontario" } => 1.13M * price,
            _ => 0
        };

    public void Method()
    {
        var res = ComputePrice(new CalculateTax { CountryName = "Alberta", ProvinceTaxProperty = new ProvinceOrStateTax { ProvinceOrState = "Alberta" } }, 10);
        Console.WriteLine("Result is " + res);
    }


}