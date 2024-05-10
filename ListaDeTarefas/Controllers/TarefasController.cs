using System;
using System.Collections.Generic;
using System.Linq;
using ListaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;

namespace ListaDeTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private List<Tarefas> _tarefas = new List<Tarefas> {
            new Tarefas { Id = 1, NomeDaTarefa = "Relatório", DtaInicio = new DateTime(2024, 5, 15), DtaFim = new DateTime(2024, 5, 20) },
            new Tarefas { Id = 2, NomeDaTarefa = "Exercício II", DtaInicio = new DateTime(2024, 5, 10), DtaFim = new DateTime(2024, 5, 15) },
            new Tarefas { Id = 3, NomeDaTarefa = "Reunião Semanal", DtaInicio = new DateTime(2024, 5, 6), DtaFim = new DateTime(2024, 5, 8) }
        };

        public IActionResult Index()
        {
            ViewBag.Tarefas = _tarefas;

            var tarefasConcluidas = _tarefas.Where(t => t.Status == StatusTarefa.Concluida).ToList();
            ViewBag.TarefasConcluidas = tarefasConcluidas;

            var tarefasEmAndamento = _tarefas.Where(t => t.Status == StatusTarefa.EmAndamento).ToList();
            ViewBag.TarefasEmAndamento = tarefasEmAndamento;

            return View();
        }

        [HttpPost]
        public IActionResult Create(Tarefas tarefa)
        {
            if (ModelState.IsValid)
            {
                int proximoId = _tarefas.Count > 0 ? _tarefas.Max(t => t.Id) + 1 : 1;
                tarefa.Id = proximoId;

                _tarefas.Add(tarefa);
            }

            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _tarefas.Remove(tarefa);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var tarefa = _tarefas.FirstOrDefault(t => t.Id == id);
            if (tarefa == null)
            {
                return NotFound();
            }

            return View(tarefa);
        }

        [HttpPost]
        public IActionResult Edit(Tarefas tarefa)
        {
            if (ModelState.IsValid)
            {
                var existingTarefa = _tarefas.FirstOrDefault(t => t.Id == tarefa.Id);
                if (existingTarefa != null)
                {
                    existingTarefa.NomeDaTarefa = tarefa.NomeDaTarefa;
                    existingTarefa.DtaInicio = tarefa.DtaInicio;
                    existingTarefa.DtaFim = tarefa.DtaFim;
                }
                return RedirectToAction("Index");
            }
            return View(tarefa);
        }

        public IActionResult TarefasAndamento()
        {
            return View("TarefasAndamento");
        }

        public IActionResult TarefasFinalizadas()
        {
            return View("TarefasFinalizadas");
        }
    }
}

