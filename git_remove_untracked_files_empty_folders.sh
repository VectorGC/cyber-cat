# Показываем кандидатов на удаление.
# https://stackoverflow.com/questions/10074582/how-do-i-remove-an-empty-folder-and-push-that-change
git clean -fdn

read -p "Вы уверены, что хотите удалить эти файлы? (Y/N) " -n 1 -r
echo    # (optional) move to a new line
if [[ ! $REPLY =~ ^[Yy]$ ]]
then
    exit 1
fi

git clean -fd
echo "Файлы удалены, нажмите любую клавишу, чтобы выйти."

$SHELL