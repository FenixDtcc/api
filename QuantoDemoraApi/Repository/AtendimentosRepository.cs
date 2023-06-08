﻿using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using QuantoDemoraApi.Data;
using QuantoDemoraApi.Models;
using QuantoDemoraApi.Repository.Interfaces;

namespace QuantoDemoraApi.Repository
{
    public class AtendimentosRepository : IAtendimentosRepository
    {
        private static readonly ILog _logger = LogManager.GetLogger("Atendimentos Repository");
        private readonly DataContext _context;
        public AtendimentosRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Atendimento>> GetAllAsync()
        {
            try
            {
                List<Atendimento> lista = await _context.Atendimentos.ToListAsync();
                return lista;
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }

        public async Task<List<Atendimento>> GetAtendimentoByHospitalIdAsync(int hospitalId)
        {
            try
            {
                List<Atendimento> lista = await _context.Atendimentos
                    .Where(x => x.IdHospital == hospitalId).ToListAsync();

                if (lista.IsNullOrEmpty())
                    throw new Exception("Hospital não encontrado, favor conferir o id informado.");

                return lista;                
            }
            catch (Exception ex)
            {
                _logger.Info(ex);
                throw;
            }
        }
    }
}

