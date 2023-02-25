namespace ProjetoHexagonal.Seguranca.Domain
{
    public class SenhaDigeridor
    {
        private static readonly string MASCARA = "#$%$^%*@\x0D\x0C";

        public string Digerir(string senha)
        {
            try
            {
                var resultado = string.Empty;

                for (var i = 0; i < senha.Length; i++)
                {
                    resultado += Convert.ToChar(Convert.ToByte(senha[i]) ^ Convert.ToByte(MASCARA[i % MASCARA.Length]));
                }

                return resultado;
            }
            catch (OverflowException ex)
            {
                throw new ArgumentException("Senha possui caractere(s) inválido(s).", ex);
            }
        }
    }
}
