using System.ComponentModel;

namespace ProEventos.Domain.Enum
{
  public enum Titulo
  {
    [Description("Não Informado")]
    NaoInformado,
    [Description("Tecnólogo")]
    Tecnologo,
    [Description("Bacharel")]
    Bacharel,
    [Description("Especialista")]
    Especialista,
    [Description("Pós Graduado")]
    PosGraduado,
    [Description("Mestrado")]
    Mestrado,
    [Description("Doutorado")]
    Doutorado,
    [Description("Pós Doutorado")]
    PosDoutorado
  }
}
