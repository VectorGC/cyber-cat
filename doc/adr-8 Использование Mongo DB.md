# Использование Mongo DB
## Контекcт
Используется клиент серверная архитектура. Все важные бизнес данные, такие как задачи или код - должны храниться на сервере. Жизненный цикл этих данных - 1 олимпиада, поэтому их не будет копиться много.

Основные разработчики - это студенты, которые могут быть не знакомы с нюансами проектирования данных и с нормальными формами БД.

Так же, куратор проекта, хорошо знаком с MongoDb.

## Решение
Мы используем MongoDb, так как с ней хорошо знаком куратор проекта. Так же это база данных позволяет не использовать нормальные формы. Хранение в формате документов и json, позволяет оператору, знакомому с этим форматом - удобно смотреть и редактировать данные. Это избавляет от необходимости создание пользовательского интерфейса для оператора.

## Статус - решение принято

## Последствия
Не требуется проектировать таблицы и связи между ними и поддерживать нормальные формы хранения в БД.