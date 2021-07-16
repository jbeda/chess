import subprocess
def run(args: list[str]):
    failed = False
    if subprocess.call(["dotnet", "build", "src/Chess.sln"]) > 0 and not failed:
        failed = True
    # todo: more than that
    return int(failed)