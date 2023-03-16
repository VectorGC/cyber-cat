# Cyber Cat (Кибер кот)
- Лицензия проекта: Apache v2.0 ([Kimsanabev Karim](https://www.linkedin.com/in/karim-kimsanbaev-013851203/)) и Apache v2.0([InGameCodeEditor - payment asset](https://assetstore.unity.com/packages/tools/gui/ingame-code-editor-144254)
- Live Demo: [Ссылка на itch.io](https://karim3d.itch.io/cyber-cat-serverless)

Проект, интерактивная игра для проведения олимпиад по программированию среди школьников и студентов. Формат проведения олимпиады - заочное и очное.

Если что-то не работает, документация не актуальна или есть проблемы с лицензиями, пожалуйста, свяжитесь со мной по адресу karim.kimsanbaev@gmail.com

# Демо (без сервера)
Режим 'без сервера', поэтому проверка кода - недоступна
[Ссылка на itch.io](https://karim3d.itch.io/cyber-cat-serverless)

# Лицензионные соглашения
## Apache v2.0 (Kimsanabev Karim)
## Apache v2.0 ([InGameCodeEditor - payment asset](https://forum.unity.com/threads/released-ingame-code-editor.663256/)
Ассет, используемые для редактора кода
Если вы используете этот проект, вы так же обязаны купить право на использование данного ассета. Купить можно [здесь](https://assetstore.unity.com/packages/tools/gui/ingame-code-editor-144254)
## С участниками олимпиады
При регистрации на мероприятие олимпиады - каждый участник дает согласие на предоставление нам учетных записей и пароля, который он вводить в игре

# Контент проекта
- Tutorial
- Client-Server API
- Documentation
- Code Editor
- Adventures Cat ^_^

# Внешние зависимости
- [UniTask](https://github.com/Cysharp/UniTask) для упрощения коллбэков и асинхронных запросов на сервер
- [UniRx v7.1.0](https://github.com/neuecc/UniRx) для связи логики и UI через использования RX архитектуры
- [Json .NET for Unity](https://assetstore.unity.com/packages/tools/input-management/json-net-for-unity-11347) для сериализации клиен-серверных запросов
- [RestClient v2.6.2](https://github.com/proyecto26/RestClient) менеджмент клиент-серверных запросов. Содержит кастомный патч, чтобы работал продвинутым Json пакетом
- Code Editor Plugin Apache v2.0 ([InGameCodeEditor - payment asset](https://forum.unity.com/threads/released-ingame-code-editor.663256/) - редактор кода
- Web GL Support фиксы и нструменты для полей ввода для корректной работы в браузере

# Ссылки
- [Проект сервера](https://github.com/kee-reel/LATE)
- [API для общения с сервером](https://kee-reel.com/cyber-api/)
- [Коды серверных ошибок](https://github.com/kee-reel/LATEST/blob/main/web/api/errors.go)
- [Как настраивать триггеры](https://gitlab.com/karim.kimsanbaev/cyber-cat/-/wikis/%D0%9A%D0%B0%D0%BA-%D0%BF%D0%BE%D0%BB%D1%8C%D0%B7%D0%BE%D0%B2%D0%B0%D1%82%D1%8C%D1%81%D1%8F-%D1%82%D1%80%D0%B8%D0%B3%D0%B3%D0%B5%D1%80%D0%B0%D0%BC%D0%B8)
- [Модальные окна](https://gitlab.com/karim.kimsanbaev/cyber-cat/-/wikis/%D0%9C%D0%BE%D0%B4%D0%B0%D0%BB%D1%8C%D0%BD%D1%8B%D0%B5-%D0%BE%D0%BA%D0%BD%D0%B0)

# UI, арт контент
- [Slim UI](https://assetstore.unity.com/publishers/35968) - с него брался и дорабатывался UI
- [Snapshot Shaders](https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/snapshot-shader-collection-146666) - неоновый эффект. Шейдеры дописывались, чтобы сделать их легкими для WebGL
- [Modal Dialog](https://assetstore.unity.com/packages/tools/gui/modal-dialog-78454) - модальные диалоги

# Авторский контент проекта
- Система триггеров на которой построен туториал


# Как запустить
1. Установите Unity версии 2020.3.27f ([тут](https://unity3d.com/ru/unity/whats-new/2020.3.27) нажмите на Install this version with Unity Hub или найдите версию и установите самостоятельно [здесь](https://unity3d.com/ru/get-unity/download/archive))
2. Склонируйте репозиторий
3. Откройте
4. Запустите сцену StartScene

# Типичный жизненный цикл игровой сессии или Core геймплей
1. При запуске игры осуществляется аутентификация. На сервер осуществляет авторизация и пользователь получает от сервера токен. По токену - сервер идентифицирует пользователя
2. Игрок смотрит комикс, после попадает в главное меню.
3. Игрок проходит обучение
4. Игрок нажимает играть и переходит в основную игру
5. Игрок находит интерактивный объект, либо квест направляет игрока к этому объекту
6. Игрок переходит в редактор кода и решает задачу успешно или не успешно
7. Игрок закрывает редактор
8. Игрок нажимает Escape -> Задачи и видит сколько задач он решил, сколько баллов заработал и сколько ему осталось решить
9. Игрок повторяет действия с пункта 5, пока не решит все задачи. Тем самым получается основной геймплейный цикл.

# Античит
Реализована мера вхождение с разных IP адресов. Если пользователь запрашивает токен или выполняет действия с токеном с IP адреса, который отличается от IP при регистрации учетной записи. Сервер выдаст ошибку 304, а на почту придет письмо с намерением подтвердить смену IP адреса.
Таким образом, можно контролировать, чтобы участники не могли помогать друг друг. В случае заочной олимпиады, это осложняет нечестное прохождение олимпиады.

# Описание архитектуры
Архитектура клиент-сервер, где сервер главный.
Клиент отвечает за геймплейное представление олимпиады.
Сервер отвечает за компиляцию и тестирования кода, а так же хранение учетных записей и доступов.

Используется UniTask и UniRx
## Модули
Основные модули вынесены в отдельные asmdef'ы, но не все
### Scripts/TaskUnits
Модели данных заданий для решения. Начинать смотреть можно с интерфейса ITaskData.
### Scripts/TaskChecker
Проверка заданий.
### Scripts/Triggers
Универсальная логика для создания квестов и интерактивных объектов.
### Scripts/Authentication
### RestAPIWrapper
Обертка для запросов на сервер. Если объявлена директива SERVERLESS - то будет использовать режим 'без сервера'.
### GameCodeEditor
Редактор кода
### CodeEditorLogger
Логгер в редакторе кода для отображения результатов проверок и ошибок.
### Cartoons
Показ комикса во вступлении
### LoadingScreen
Экран загрузки
### TaskProgressBoard
Доска задач, вызывается Escape -> Задачи. Отображает прогресс по всем задачам

## Слой представления
### Сцена Game
Основная сцена в которой происходит игра

# Настройка задач
Задачи приходят с сервера. Каждый интерактивный объект на сцене содержит или имеет связь со скриптом TaskFromFolderView. Этот скрипт задает по какому пути в репозитории задач брать задачу.
Подробнее про репозитории задач и сервер можно ознакомится [здесь](https://github.com/kee-reel/LATE).
Сам репозиторий с задачами [здесь](https://github.com/kee-reel/late-sample-project)

## Чтобы реализовать свой сервер
Нужно реализоваться IRestAPI на клиенте и реализовать запросы, согласно формату [здесь](https://kee-reel.com/cyber-api/)

# Другие участники проекта и контрибьюторы

| ФИО | Роль | Контакты для связи |
| ----------------------------- | ----------------------------- | ----------------------------- |
| Кимсанбаев Карим | Team Lead | https://www.linkedin.com/in/karim-kimsanbaev-013851203/ или karim.kimsanbaev@gmail.com |
| Крылов Кирилл | Backend Programmer | https://kee-reel.com/ |
| Карпинский Артем (И904Б)    | Programmer of Gameplay | artem19051664@gmail.com |
| Ермолаев Святослав (И503Б)  | Programmer of code editor | ledumblasphemus@gmail.com |
| Востриков Виталий (И594)    | 3D Artist | talytriko@gmail.com |
| Слава Снегирев (И508Б)      | Leve Designer | slavick.snegirev@icloud.com |
| Миша Лукашев (И507Б)        | Game Designer | boyskyfall@vk.com |
