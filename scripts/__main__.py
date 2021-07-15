from scripts import SCRIPTS
import sys
if len(sys.argv) < 2:
    print("No command specified!")
    exit(1)
command = sys.argv[1]
try:
    command_function = SCRIPTS[command]
    print("Running command: {}".format(command))
    exit(command_function(sys.argv[2:]))
except KeyError:
    print("Could not find command: {}".format(command))
    exit(1)