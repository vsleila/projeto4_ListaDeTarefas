using ListaDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;


namespace ListaDeTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private static List<Tarefas> _tarefas = new List<Tarefas>() {
            new Tarefas { Id = 1, NomeDaTarefa = "Relatório", DtaInicio = new DateTime(2024, 5, 15), DtaFim = new DateTime(2024, 5, 20) },
            new Tarefas { Id = 2, NomeDaTarefa = "Exercício II", DtaInicio = new DateTime(2024, 5, 10), DtaFim = new DateTime(2024, 5, 15) },
            new Tarefas { Id = 3, NomeDaTarefa = "Reunião Semanal", DtaInicio = new DateTime(2024, 5, 6), DtaFim = new DateTime(2024, 5, 8) }
        };

        public IActionResult Index()
        {
            return View(_tarefas);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Tarefas tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.Id = _tarefas.Count > 0 ? _tarefas.Max(t => t.Id) + 1 : 1;
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

        public IActionResult Details(int id)
        {
            var task = _tarefas.FirstOrDefault(t => t.Id == id);
            if (task == null)
            {
                return NotFound();
            }
            return View(task);
        }

        public IActionResult TarefasAndamento()
        {
            var tarefasEmAndamento = _tarefas.Where(t => t.Status == StatusTarefa.EmAndamento).ToList();
            return View(tarefasEmAndamento);
        }

        public IActionResult TarefasFinalizadas()
        {
            var tarefasFinalizadas = _tarefas.Where(t => t.Status == StatusTarefa.Concluida).ToList();
            return View(tarefasFinalizadas);
        }
    }
}

