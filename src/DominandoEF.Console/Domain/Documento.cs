namespace DominandoEF.Domain
{
    public class Documento
    {
        public int Id { get; set; }

        private string _cpf;

        public void SetCpf(string cpf)
        {
            if (string.IsNullOrEmpty(cpf))
            {
                throw new System.Exception("Cpf Inválido");
            }
            _cpf = cpf;
        }

        public string GetCpf() => _cpf;




    }
}
