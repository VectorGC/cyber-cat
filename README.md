# Cyber Cat (Кибер кот)
- Лицензия проекта:
Apache v2.0 ([Kimsanabev Karim](https://www.linkedin.com/in/karim-kimsanbaev-013851203/))
Apache v2.0([InGameCodeEditor - payment asset](https://assetstore.unity.com/packages/tools/gui/ingame-code-editor-144254)

- Live Demo: [Ссылка на itch.io](https://karim3d.itch.io/cyber-cat-programming-game)

Проект, интерактивная игра для проведения олимпиад по программированию среди школьников и студентов. Формат проведения олимпиады - заочное и очное.

Если что-то не работает, документация не актуальна или есть проблемы с лицензиями, пожалуйста, свяжитесь со мной по адресу karim.kimsanbaev@gmail.com

# Как запустить без сервера (не будет работать комплияция и выполнение кода)
1. Установите Unity версии 2020.3.27f ([тут](https://unity3d.com/ru/unity/whats-new/2020.3.27) нажмите на Install this version with Unity Hub или найдите версию и установите самостоятельно [здесь](https://unity3d.com/ru/get-unity/download/archive))
2. Склонируйте репозиторий
3. Откройте
4. Возле кнопки "Воспроизведения" в Unity - выберите Serverless.
5. Запустите сцену MainScene

# Как запустить проект с сервером
1. Установите [докер декстоп](https://www.docker.com/products/docker-desktop/)
2. Выполните backend/CyberCatServer/start_dev_server_in_docker.sh
3. Сервер будет доступен на localhost:80
4. В проект в Unity
5. Возле кнопки "Воспроизведения" в Unity - выберите Localhost
6. Запустите сцену MainScene

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
- Code Editor Plugin Apache v2.0 ([InGameCodeEditor - payment asset](https://forum.unity.com/threads/released-ingame-code-editor.663256/) - редактор кода
- Web GL Support фиксы и нструменты для полей ввода для корректной работы в браузере
- [Panda BehTree Free](https://assetstore.unity.com/packages/tools/behavior-ai/panda-bt-free-33057) для реализации логики и квестов черех бехейвор деревья

# UI, арт контент
- [Slim UI](https://assetstore.unity.com/publishers/35968) - с него брался и дорабатывался UI
- [Snapshot Shaders](https://assetstore.unity.com/packages/vfx/shaders/fullscreen-camera-effects/snapshot-shader-collection-146666) - неоновый эффект. Шейдеры дописывались, чтобы сделать их легкими для WebGL
- [Modal Dialog](https://assetstore.unity.com/packages/tools/gui/modal-dialog-78454) - модальные диалоги

# Описание архитектуры
Архитектура клиент-сервер, где сервер главный.
Клиент отвечает за геймплейное представление олимпиады.
Сервер отвечает за компиляцию и тестирования кода, а так же хранение учетных записей и доступов.

# Участники проекта и контрибьюторы

| ФИО | Роль | Контакты для связи |
| ----------------------------- | ----------------------------- | ----------------------------- |
| Кимсанбаев Карим | Team Lead | https://www.linkedin.com/in/karim-kimsanbaev-013851203/ или karim.kimsanbaev@gmail.com |
| Крылов Кирилл | Backend Programmer | https://kee-reel.com/ |
| Карпинский Артем (И904Б)    | Programmer of Gameplay | artem19051664@gmail.com |
| Пекуш Даниил                | Programmer of Server (Backend) | - |
| Ермолаев Святослав (И503Б)  | Programmer of code editor | ledumblasphemus@gmail.com |
| Востриков Виталий (И594)    | 3D Artist | talytriko@gmail.com |
| Слава Снегирев (И508Б)      | Leve Designer | slavick.snegirev@icloud.com |
| Миша Лукашев (И507Б)        | Game Designer | boyskyfall@vk.com |
