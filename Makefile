# var
MODULE  = $(notdir $(CURDIR))

# dirs
CWD = $(CURDIR)
BIN = $(CWD)/bin
DOC = $(CWD)/doc
SRC = $(CWD)/src
TMP = $(CWD)/tmp
GZ  = $(HOME)/gz

# tool
CURL   = curl -L -o
CF     = clang-format -style=file -i
DOT    = /usr/bin/dotnet
PY     = $(BIN)/python3
PIP    = $(BIN)/pip3

# src
C += $(wildcard src/*.c*)
H += $(wildcard inc/*.h*)
F += $(wildcard lib/*.ini) $(wildcard lib/*.f)

# cfg
CFLAGS += -Iinc -Itmp

# all
.PHONY: all
all: bin/$(MODULE) $(F)
	$^

# format
.PHONY: format
format: tmp/format_cpp
tmp/format_cpp: $(C) $(H)
	$(CF) $? && touch $@

# rule
bin/$(MODULE): $(C) $(H)
	$(CXX) $(CFLAGS) -o $@ $(C) $(L)

# install
.PHONY: install update
install: /etc/apt/sources.list.d/microsoft-prod.list
	$(MAKE) update
update: $(PIP)
	sudo apt update
	sudo apt install -uy `cat apt.txt`
	$(PIP) install -U -r requirements.txt

/etc/apt/sources.list.d/microsoft-prod.list: $(GZ)/packages-microsoft-prod.deb
	sudo dpkg -i $<

$(GZ)/packages-microsoft-prod.deb:
	$(CURL) $@ https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb

$(PY) $(PIP):
	python3 -m venv .


$(MODULE).fsproj:
	dotnet new console --language F#

lab:
	dotnet interactive jupyter install
