using Microsoft.IdentityModel.Tokens;
using Transport_Shadule.Models;

namespace Transport_Shadule
{
    internal class Program
    {
        static void Print<T>(string sqlText, IEnumerable<T>? items)
        {
            Console.WriteLine(sqlText);
            Console.WriteLine("Записи: ");
            if (items != null && items.Any())
            {
                foreach (var item in items)
                {
                    Console.WriteLine(item!.ToString());
                }
            }
            else
            {
                Console.WriteLine("Пусто");
            }
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу");
            Console.ReadKey();
        }

        static void Select(TransportShaduleContext db)
        {
            // 3.2.1. Выборка всех данных из таблицы на стороне отношения "один" (Маршруты)
            var queryLINQ1 = from r in db.Routes
                             select new
                             {
                                 НаименованиеМаршрута = r.RouteName,
                                 ТипТранспорта = r.VehicleType,
                                 ПлановоеВремя = r.PlannedTravelTime,
                                 Расстояние = r.Distance
                             };
            string comment = "1. Выборка всех данных из таблицы маршрутов (на стороне отношения 'один')";
            Print(comment, queryLINQ1.Take(10).ToList());

            // 3.2.2. Выборка с условием (выбор экспресс-маршрутов)
            var queryLINQ2 = from r in db.Routes
                             where r.IsExpress == true
                             select new
                             {
                                 НаименованиеМаршрута = r.RouteName,
                                 ТипТранспорта = r.VehicleType,
                                 ПлановоеВремя = r.PlannedTravelTime,
                                 Экспресс = r.IsExpress
                             };
            comment = "2. Выборка экспресс-маршрутов";
            Print(comment, queryLINQ2.ToList());

            // 3.2.3. Выборка с группировкой по типу транспорта и подсчетом количества маршрутов для каждого типа
            var queryLINQ3 = from r in db.Routes
                             group r by r.VehicleType into g
                             select new
                             {
                                 ТипТранспорта = g.Key,
                                 КоличествоМаршрутов = g.Count(),
                             };

            // Комментарий к запросу
            comment = "3. Выборка по типу транспорта с подсчетом количества маршрутов";
            Print(comment, queryLINQ3.ToList());

            // 3.2.4. Выборка данных из двух таблиц (Остановки и Расписание)
            var queryLINQ4 = db.Stops
                               .Join(db.Schedules, st => st.StopId, sch => sch.StopId, (st, sch) => new
                               {
                                   Остановка = st.StopName,
                                   ВремяПрибытия = sch.ArrivalTime
                               });
            comment = "4. Выборка остановок и времени прибытия транспорта";
            Print(comment, queryLINQ4.Take(10).ToList());

            // 3.2.5. Фильтрация данных (Расписание по определенному дню и остановке)
            var queryLINQ5 = from sch in db.Schedules
                             join st in db.Stops on sch.StopId equals st.StopId
                             where sch.DayOfWeek == "Monday" && st.StopName == "Main Street"
                             select new
                             {
                                 Остановка = st.StopName,
                                 ВремяПрибытия = sch.ArrivalTime,
                                 ДеньНедели = sch.DayOfWeek
                             };
            comment = "5. Выборка расписания по остановке 'Main Street' и дню недели 'Monday'";
            Print(comment, queryLINQ5.Take(10).ToList());
        }

        static void Insert(TransportShaduleContext db)
        {
            // 3.2.6. Вставка данных в таблицу "Остановки"
            Stop stop = new Stop
            {
                StopName = "New Stop",
                IsTerminal = false,
                HasDispatcher = true
            };
            db.Stops.Add(stop);
            db.SaveChanges();
            string comment = "Выборка остановок после вставки новой остановки";
            var queryLINQ1 = from s in db.Stops
                             where s.StopName == "New Stop"
                             select new
                             {
                                 НазваниеОстановки = s.StopName,
                                 ЯвляетсяКонечной = s.IsTerminal,
                                 НаличиеДиспетчера = s.HasDispatcher
                             };
            Print(comment, queryLINQ1.ToList());

            // 3.2.7. Вставка данных в таблицу "Маршруты"
            Route route = new Route
            {
                RouteName = "New Route",
                VehicleType = "Bus",
                PlannedTravelTime = new TimeOnly(1, 0),
                Distance = 15,
                IsExpress = false
            };
            db.Routes.Add(route);
            db.SaveChanges();
            comment = "Выборка маршрутов после вставки нового маршрута";
            var queryLINQ2 = from r in db.Routes
                             where r.RouteName == "New Route"
                             select new
                             {
                                 НаименованиеМаршрута = r.RouteName,
                                 ТипТранспорта = r.VehicleType,
                                 ПлановоеВремя = r.PlannedTravelTime
                             };
            Print(comment, queryLINQ2.ToList());
        }

        static void Delete(TransportShaduleContext db)
        {
            // 3.2.8. Удаление записи из таблицы "Остановки"
            string stopName = "New Stop";
            var stop = db.Stops.Where(s => s.StopName == stopName).FirstOrDefault();

            if (stop != null)
            {
                db.Stops.Remove(stop);
                db.SaveChanges();
            }
            string comment = "Выборка остановок после удаления остановки";
            var queryLINQ1 = from s in db.Stops
                             where s.StopName == "New Stop"
                             select new
                             {
                                 НазваниеОстановки = s.StopName
                             };
            Print(comment, queryLINQ1.ToList());

            // 3.2.9. Удаление записи из таблицы "Маршруты"
            string routeName = "New Route";
            var route = db.Routes.Where(r => r.RouteName == routeName).FirstOrDefault();

            if (route != null)
            {
                db.Routes.Remove(route);
                db.SaveChanges();
            }
            comment = "Выборка маршрутов после удаления маршрута";
            var queryLINQ2 = from r in db.Routes
                             where r.RouteName == "New Route"
                             select new
                             {
                                 НаименованиеМаршрута = r.RouteName
                             };
            Print(comment, queryLINQ2.ToList());
        }

        static void Update(TransportShaduleContext db)
        {
            // 3.2.10. Обновление данных
            int routeId = 1; // Пример обновления по ID
            var route = db.Routes.Where(r => r.RouteId == routeId).FirstOrDefault();

            if (route != null)
            {
                route.Distance = 20; // Обновляем расстояние
                db.SaveChanges();
            }

            string comment = "Выборка маршрутов после обновления";
            var queryLINQ1 = from r in db.Routes
                             where r.RouteId == routeId
                             select new
                             {
                                 НаименованиеМаршрута = r.RouteName,
                                 Расстояние = r.Distance
                             };
            Print(comment, queryLINQ1.ToList());
        }

        static void Main(string[] args)
        {
            using (var db = new TransportShaduleContext())
            {
                Console.WriteLine("Выполняется выборка данных...");
                Select(db);

                Console.WriteLine("Выполняется вставка данных...");
                Insert(db);

                Console.WriteLine("Выполняется удаление данных...");
                Delete(db);

                Console.WriteLine("Выполняется обновление данных...");
                Update(db);
            }
        }
    }
}
