chcp 1251
set cyber_cat_server_path=%~dp0

start %cyber_cat_server_path%\start_local_mongo.bat
start %cyber_cat_server_path%\ApiGateway\ApiGateway\start_api_gateway_local.bat
start %cyber_cat_server_path%\CppLauncherService\CppLauncherService\start_cpp_launcher_local.bat
start %cyber_cat_server_path%\AuthService\AuthService\start_auth_service_local.bat
start %cyber_cat_server_path%\JudgeService\JudgeService\start_judge_service_local.bat
start %cyber_cat_server_path%\PlayerService\PlayerService\start_player_service_local.bat
start %cyber_cat_server_path%\TaskService\TaskService\start_task_service_local.bat

