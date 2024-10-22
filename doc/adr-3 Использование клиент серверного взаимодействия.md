# Использование клиент серверного взаимодействия
## Контекcт
Основная цель проекта, это удаленное и автоматизированное проведения олимпиад для школьников и студентов. Код, который пишут пользователи должен легко доставаться из приложения. Задачи, должны оперативно редактироваться. Проблемы зачастую будут обнаружены уже в процессы проведения олимпиад, из-за чего обновления и исправления ошибок должны доставляться в приложение как можно быстрее.

## Решение
Мы используем клиент-серверное взаимодействие. Где весь критичный функционал помещается на сервере. Если клиент по каким-то причинам имеет критическую ошибку и на нем невозможно осуществить проведение олимпиады. Клиентом выступает отдельный сайт, который реализует связь с сервером вместо основной игры на Unity.

Таким образом мы можем полностью отказаться от клиента Unity, если в процессе проведение олимпиады возникает критическая ошибка.

## Статус - решение принято

## Последствия
Вся важная бизнес логика для проведения олимпиад хранится на сервере. Это позволяет оперативно менять задания и отслеживать действия игроков в реальном времени. Тем самым становится возможно оказать оперативную, удаленную поддержку игрокам.

Клиент не содержит важной бизнес логики, чтобы сильно упрощает кодовую базу самой игры на Unity.

Unity связан только с сервером и не общается с базой данных или другими системами напрямую.

### Однако
Требуется проектировать клиент-серверное взаимодействие.

Есть дублирующийся код между клиентом и сервером в описании Dto сущностей.

Требуется настройка и поддержание локального сервера в процессе разработки. Либо разработка и поддержка serverless режиме.

Требуются дополнительные методы развертки и настройки сервера.