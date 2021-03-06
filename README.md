## Задачи
:heavy_check_mark: 1. (deadline - 19.05.2019) Разработать систему типов для описания работы с банковским счетом (в соответствии с принципами SOLID). Состояние счета определяется его номером, данными о владельце счета (имя, фамилия, e-mail), суммой на счете и некоторыми бонусными баллами, которые увеличиваются/уменьшаются каждый раз при пополнении счета/списании со счета на величины различные для пополнения и списания и рассчитываемые в зависимости от некоторых значений величин «стоимости» баланса и «стоимости» пополнения. Величины «стоимости» баланса и «стоимости» пополнения являются целочисленными значениями и зависят от типа счета, который может быть, например, Base, Gold, Platinum. Для реализации системы типов для счетов использовать паттерн "Шаблонный метод". Для работы со счетом реализовать следующие возможности:

1. выполнить пополнение на счет;

2. выполнить списание со счета;

3. перевести сумму на другой счет;

4. создать новый счет;

5. закрыть счет.

При проектировании типов учитывать следующие возможности расширения/изменения функциональности:

1. добавление новых видов счетов;

2. изменение/добавление источников хранения информации о счетах;

3. изменение расчета бонусных баллов в производных классах;

4. изменении логики генерации номера счета.

Для хранения информации о счетах использовать fake-имплементацию репозитория (хранилища) в виде класса-обертки для коллекции List.

Работу классов продемонстрировать на примере консольного приложения.

Для организации классов и интерфейсов использовать “The Stairway pattern” (“заготовка” в архиве AccountSystemDemo.7z, пример ProjectArchitectureDemo.7z).

Для разрешения зависимостей использовать Ninject- фреймворк.

Реализовать возможность логирования сообщений различного уровня, предусмотрев возможность использования различных фреймворков для логирования (в качестве текущего использовать NLog - фреймворк).

Протестировать слой Bll (NUnit и Moq фреймворки).

:grey_exclamation: 2. (deadline - 15.05.2019) Создать обобщенные классы для представления квадратной, симметрической и диагональной матриц (симметрическая матрица – это квадратная матрица, которая совпадает с транспонированной к ней; диагональная матрица – это квадратная матрица, у которой элементы вне главной диагонали имеют значения по умолчанию типа элемента). 

Описать в созданных классах событие, которое происходит при изменении элемента матрицы с индексами (i, j). 

Расширить функциональность существующей иерархии классов, добавив возможность операции сложения двух матриц любого типа (рассмотреть два возможный варианта). Разработать unit-тесты.
