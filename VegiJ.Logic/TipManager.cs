﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VegiJ.DataAccess;
using VegiJ.DataAccess.Contracts;

namespace VegiJ.Logic
{
    public class TipManager : ITipManager
    {
        private IRepository<Tip> _tipRepository;

        public TipManager(IRepository<Tip> repo)
        {
            _tipRepository = repo;
        }
        public void AddTip(Tip tip)
        {
            if (this.TipNameExist(tip.Title) || this.TipIDExist(tip.ID))
            {
                throw new ArgumentException("Tip already exist!");
            }

            _tipRepository.Create(tip);
        }

        public void DeleteTip(Tip tip)
        {
            if (!this.TipIDExist(tip.ID))
            {
                throw new ArgumentException("Tip do not exist!");
            }
            _tipRepository.Delete(tip.ID);
        }

        public IQueryable<Tip> GetAllTips()
        {
            return _tipRepository.Table;
        }

        public Tip GetTip(Guid Id)
        {
            return _tipRepository.GetById(Id);
        }

        public void UpdateTip(Tip tip)
        {
            if (!this.TipIDExist(tip.ID))
            {
                throw new ArgumentException("Tip do not exist!");
            }

            _tipRepository.Update(tip);
        }

        private bool TipIDExist(Guid id)
        {
            return _tipRepository.GetById(id) != null;
        }

        private bool TipNameExist(string name)
        {
            return _tipRepository.Table.Any(c => c.Title.Equals(name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
