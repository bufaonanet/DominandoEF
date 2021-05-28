using Microsoft.EntityFrameworkCore;
using System;

namespace DominandoEF.Tips.Domain
{
    [Keyless]
    public class UsuarioFuncao
    {
        public Guid UsuarioId { get; set; }
        public Guid FuncaoId { get; set; }        
    }
}
