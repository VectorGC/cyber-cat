# Использование WebGL
## Контекcт
Целевая аудитория проекта, это школьники. Ребята, которые, возможно, технически плохо подкованы в вопросах работы с операционной системой. Для них процесс должен быть максимально легким и быстрым. Чтобы нажали на кнопочки и сразу играть.

Приложение так же планируется использовать для удаленного проведения олимпиад, то есть в условиях, когда игроку-школьнику, никто не может помочь решить его технические проблемы.

Оперативной системы реагирования на проблемы у игроков играющих удаленно - нету. Поэтому UX по установке и запуску должен быть максимально простым и понятным.

## Решение
Мы решили использовать WebGL, чтобы игра работала в браузере. Так процесс выглядит максимально дрежулюбным для пользователя. Открыл ссылку - нажал играть.

## Статус - решение принято

## Последствия
У игроков не возникает вопросов и проблем с запуском игры.

Если в процессе проведения олимпиады возникают проблемы - Web GL обеспечивает оперативную доставку обновлений и хот фиксов на прод. Игроки получают исправленную версию максимально быстро.

### Однако
WebGL слабо поддерживается компанией Unity. А сама технология относительно старая. Из-за этого возникают проблемы с разработкой и поддержкой новых фичей. Например, регистрация в Unity или редактор кода - для них используется специальный ассет для WebGL, который исправляет баги с вводом с клавиатуры.

Не работает ctrl+c, ctrl+v, что очень критично для процессы написания кода с точки зрения игроков. Это **сильно портит** UX.

Графические возможности и оптимизация игры ограничена браузером. Это заставляет более пристально следить за производительностью. 