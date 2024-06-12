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

# src
C += $(wildcard src/*.c*)
H += $(wildcard inc/*.h*)

# cfg
CFLAGS += -Iinc -Itmp

# all
.PHONY: all
all: bin/$(MODULE)
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
update:
	sudo apt update
	sudo apt install -uy `cat apt.txt`

/etc/apt/sources.list.d/microsoft-prod.list: $(GZ)/packages-microsoft-prod.deb
	sudo dpkg -i $<

$(GZ)/packages-microsoft-prod.deb:
	$(CURL) $@ https://packages.microsoft.com/config/debian/12/packages-microsoft-prod.deb
