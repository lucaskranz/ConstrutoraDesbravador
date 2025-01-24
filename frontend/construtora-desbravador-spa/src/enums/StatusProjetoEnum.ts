export enum StatusProjetoEnum{
    EmAnalise,
    AnaliseRealizada, 
    AnaliseAprovada,
    Iniciado,
    Planejado,
    EmAndamento,
    Encerrado,
    Cancelado
}

export const StatusProjetoEnumDescription = {
    [StatusProjetoEnum.EmAnalise]: "Em Análise",
    [StatusProjetoEnum.AnaliseRealizada]: "Análise Realizada",
    [StatusProjetoEnum.AnaliseAprovada]: "Análise Aprovada",
    [StatusProjetoEnum.Iniciado]: "Iniciado",
    [StatusProjetoEnum.Planejado]: "Planejado",
    [StatusProjetoEnum.EmAndamento]: "Em Andamento",
    [StatusProjetoEnum.Encerrado]: "Encerrado",
    [StatusProjetoEnum.Cancelado]: "Cancelado",
};