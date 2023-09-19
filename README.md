# Cyber Cat (Кибер кот)
## [Играй сейчас в Кибер кот на itch.io](https://karim3d.itch.io/cyber-cat-programming-game)
Лицензии:
  - Apache v2.0 ([Kimsanabev Karim](https://www.linkedin.com/in/karim-kimsanbaev-013851203/))
  - Apache v2.0([InGameCodeEditor - payment asset](https://assetstore.unity).com/packages/tools/gui/ingame-code-editor-144254)

Игра с решением олимпиадных задач по программированию на языке Си. Вы играете котом в университете, помогаете студентами  преподавателям - решая задачи на языке Си.

Если что-то не работает, документация не актуальна или есть проблемы с лицензиями, пожалуйста, свяжитесь со мной по адресу karim.kimsanbaev@gmail.com

## Как запустить без сервера (не будет работать комплияция и выполнение кода)
1. Установите Unity версии 2020.3.27f ([тут](https://unity3d.com/ru/unity/whats-new/2020.3.27) нажмите на Install this version with Unity Hub или найдите версию и установите самостоятельно [здесь](https://unity3d.com/ru/get-unity/download/archive))
2. Склонируйте репозиторий
3. Откройте
4. Возле кнопки "Воспроизведения" в Unity - выберите Serverless.
5. Запустите сцену MainScene

## Как запустить проект с сервером в докере
1. Установите [докер декстоп](https://www.docker.com/products/docker-desktop/)
2. Выполните backend/CyberCatServer/start_dev_server_in_docker.sh
3. Сервер будет доступен на localhost:80
4. В Unity
5. Возле кнопки "Воспроизведения" в Unity в выпдпающем списке - выберите Localhost
6. Запустите сцену MainScene

## Архитектура
Клиент-серверная архитекутра. Клиент - Unity. Сервер - Asp Net Core (микросервисы). Большая часть кода покрыта тестами. Почти весь серверный код покрыт тестами
- Клиент
    - Unity
    - UniTask
    - Zenject
    - Bonsai Behaviour Tree (деревья поведений, подключен как саб репозиторий)
    - ApiGateway.Client.dll (SDK для связи с сервером)
    - InGameCodeEditor (платный ассет, редактор кода)
    - Тесты, в том числе и тесты ApiGateway.Client.Tests.dll запускается внутри Unity
- Сервер
    - Комплияция и выполнение кода на языке Си
    - MongoDb
    - Авторизация через JWT токены
    - Docker
    - Grpc (protobuf-net)
    - E2E (ApiGateway.Client.Tests.dll) и интеграционные для отдельных микросервисов

## Apache v2.0 (Kimsanabev Karim)
## Apache v2.0 ([InGameCodeEditor - payment asset](https://forum.unity.com/threads/released-ingame-code-editor.663256/)
Ассет, используемые для редактора кода
Если вы используете этот проект, вы так же обязаны купить право на использование данного ассета. Купить можно [здесь](https://assetstore.unity.com/packages/tools/gui/ingame-code-editor-144254)

# Участники проекта и контрибьюторы

| ФИО                        | Роль | Контакты для связи                                                                                 |
|----------------------------| ----------------------------- |----------------------------------------------------------------------------------------------------|
| Кимсанбаев Карим           | Team Lead | [LimkedIn](https://www.linkedin.com/in/karim-kimsanbaev-013851203/) или karim.kimsanbaev@gmail.com |
| Крылов Кирилл              | Backend Programmer | https://kee-reel.com/                                                                              |
| Карпинский Артем (И904Б)   | Programmer of Gameplay | artem19051664@gmail.com                                                                            |
| Пекуш Даниил               | Programmer of Server (Backend) | dap0602@mail.ru                                                                                    |
| Ермолаев Святослав (И503Б) | Programmer of code editor | ledumblasphemus@gmail.com                                                                          |
| Востриков Виталий (И594)   | 3D Artist | talytriko@gmail.com                                                                                |
| Слава Снегирев (И508Б)     | Leve Designer | slavick.snegirev@icloud.com                                                                        |
| Миша Лукашев (И507Б)       | Game Designer | boyskyfall@vk~~~~.com                                                                              |
