using System.ComponentModel;

namespace ProEventos.Domain.Enum
{
  public enum Funcao
  {
    [Description("Não Informado")]
    NaoInformado,
    [Description("Participante")]
    Participante,
    [Description("Palestrante")]
    Palestrante
  }
}