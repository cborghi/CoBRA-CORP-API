using CoBRA.Domain.Entities;
using CoBRA.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace CoBRA.Infra.Intranet
{
    public class MenuRepository : BaseRepository, IMenuRepository
    {
        public MenuRepository(IConfiguration configuration) : base(configuration)
        {

        }

        public List<Menu> ObterMenus(bool subMenus)
        {
            try
            {

                List<Menu> menus = new List<Menu>();

                string queryMenuPai = subMenus ? "" : $"WHERE ID_MENU_PAI IS NULL";

                string query = $@"SELECT ID_MENU, ID_MENU_PAI, DESCRICAO, LINK, ROTA, ATIVO FROM MENU {queryMenuPai}";

                using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            menus.Add(new Menu()
                            {
                                Id = Convert.ToInt32(dataReader["ID_MENU"]),
                                IdPai = dataReader["ID_MENU_PAI"] != DBNull.Value ? Convert.ToInt32(dataReader["ID_MENU_PAI"]) : (int?)null,
                                Descricao = dataReader["DESCRICAO"].ToString(),
                                Link = dataReader["LINK"].ToString(),
                                Rota = dataReader["ROTA"].ToString(),
                                Ativo = Convert.ToBoolean(dataReader["ATIVO"]),
                            });
                        };
                    };
                };
                return menus;
            }
            catch
            {
                throw;
            }
        }

        /*função para recuperar ids dos Menus selecionados para acesso do grupo, 
        função foi feita pois no front end não esta retornando os ids dos menus selecionados, apenas a descrição*/
        public List<Menu> ObterIdsMenus(List<Menu> listaMenus)
        {
            try
            {

                string query = $@"SELECT ID_MENU, ID_MENU_PAI, DESCRICAO, LINK, ROTA, ATIVO FROM MENU";

                using (DbDataReader dataReader = GetDataReader(query.ToString(), null, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            var descricao = dataReader["DESCRICAO"].ToString();
                            var idMenu = Convert.ToInt32(dataReader["ID_MENU"]);

                            foreach (var item in listaMenus)
                            {
                                if (descricao == item.Descricao.Trim())
                                {
                                    item.Descricao = item.Descricao.Trim();
                                    item.Id = idMenu;
                                };
                            };
                        };
                    };
                }
                return listaMenus;
            }
            catch
            {
                throw;
            }
        }

        public List<Menu> ObterMenusPorGrupoId(int IdGrupo)
        {
            try
            {
                List<Menu> listaMenus = new List<Menu>();

                string query = $@"SELECT m.ID_MENU, m.ID_MENU_PAI, m.DESCRICAO, m.LINK, m.ROTA FROM MENU m
                                    INNER JOIN MENU_GRUPO mg ON mg.ID_GRUPO = @IdGrupo
                                    where m.ID_MENU = mg.ID_MENU order by DESCRICAO";

                List<SqlParameter> listaParam = new List<SqlParameter>()
                {
                    new SqlParameter() { ParameterName = "@IdGrupo", Value = IdGrupo }
                };

                using (DbDataReader dataReader = GetDataReader(query.ToString(), listaParam, CommandType.Text))
                {
                    if (dataReader != null && dataReader.HasRows)
                    {
                        while (dataReader.Read())
                        {
                            listaMenus.Add(new Menu()
                            {
                                Id = Convert.ToInt32(dataReader["ID_MENU"]),
                                Descricao = dataReader["DESCRICAO"].ToString(),
                                Link = dataReader["LINK"].ToString(),
                                Rota = dataReader["ROTA"].ToString(),
                                IdPai = dataReader["ID_MENU_PAI"] != DBNull.Value ? Convert.ToInt32(dataReader["ID_MENU_PAI"]) : (int?)null
                            });
                        };
                    };
                }

                //ordenar menus por hierarquia
                List<Menu> menusOrdenados = new List<Menu>();

                foreach (var menu in listaMenus)
                {
                    if (menu.IdPai == null)
                    {
                        menusOrdenados.Add(menu);
                        foreach (var item in listaMenus)
                        {
                            if (item.IdPai == menu.Id)
                            {
                                menusOrdenados.Add(item);

                                foreach(var subItem in listaMenus)
                                {
                                    if(subItem.IdPai == item.Id)
                                        menusOrdenados.Add(subItem);
                                }

                            }
                        }
                    }
                }
                return menusOrdenados;
            }
            catch(Exception ex)
            {
                throw;
            }
        }
    }
}
