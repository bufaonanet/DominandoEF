using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DominandoEF.Domain;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace DominandoEF.Conversores
{
    public class ConversorCustomizado : ValueConverter<Status, string>
    {
        public ConversorCustomizado() : base(
            p => ConverterParaBancoDeDados(p),
            value => ConverterParaAplicacaoa(value),
            new ConverterMappingHints(1))
        {

        }

        static string ConverterParaBancoDeDados(Status status)
        {
            return status.ToString()[0..1];
        }

        static Status ConverterParaAplicacaoa(string value)
        {
            var status = Enum.GetValues<Status>().FirstOrDefault(p => p.ToString()[0..1] == value);

            return status;
        }
    }
}
