import subprocess
def run(args: list[str]):
    subprocess.call(["dotnet", "build", "src/Chess.sln"])
    # todo: more than that
    return 0