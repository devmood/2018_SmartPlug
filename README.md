# SmartPlug

## Zasada dzia³ania oraz krótki opis

**SmartPlug** jest projektem zaliczeniowym na zajêcia z Podstawy Techniki Mikroprocerowej, napisanym w jêzyku C. Dodatkowo, aplikacja s³u¿¹ca do komunikacji miêdzy u¿ytkownikiem a urz¹dzeniem, zosta³a napisana w C#. SmartPlug dzia³a na prostej zasadzie:

- urz¹dzenie elektroniczne (TV, lampka, suszarka, etc.) zostaje pod³¹czane do 'gniazdka',

- gniazdko 'tworzy' w³asny hotspot - otwarta sieæ **AI-THINKER_F68149**,

- mo¿na siê do niego pod³¹czyæ za pomoc¹ interfejsu aplikacji, nale¿y równie¿ podaæ przyk³adowo Adres: 192.168.4.1 oraz nr Portu: 1234,

- dodatkowo aplikacja umo¿liwia prze³¹czanie przekaŸników,

- przekaŸniki po przekroczeniu wymaganej wartoœci, prze³¹czaj¹ siê w sposób skokowy, umo¿liwiaj¹c dop³yw pr¹du lub jego odciêcie, dziêki czemu urz¹dzenie elektroniczne mo¿e siê w³¹czyæ lub wy³¹czyæ.

## Wykorzystywane podzespo³y

1.	STM32F407G Discovery

![stm32f4](https://image.ibb.co/kWcQSy/stm32f4_discovery.jpg)

2.	Modu³ Wi-Fi ESP8266-01

![esp](https://image.ibb.co/mrpVSy/HCMODU0085_800_600_New.jpg)

4.	PrzekaŸnik SRD-12VDC-SL-C

![relay](https://image.ibb.co/jzYAud/relay_module_12v_1024x1024.jpg)

## Sposób pod³¹czenia pinów

|PORT GPIO - STM32|PORT - ESP8266-01          |PORT SRD-12VDC-SL-C|
|-|-|-|
|GND|GND|GND|
|PC10 (TX)|RX|-|
|PC11 (RX)|TX|-|
|PE12|-|INPUT1|
|PE13|-|INPUT2|
||||

## Struktura projektu i opis

> \.\.\SmartGniazdko\SmartGniazdkoSTMProjekt\main.c
>>	Lokalizacja pliku *main.c*, w którym zawarta jest ca³a logika projektu, realizowana na mikrokontrolerze.
>>
>>> int main(void);
>>>> Funkcja zawieraj¹ca pêtlê nieskoñczon¹, wywo³ywane s¹ w niej równie¿ inne procedury oraz funkcje.
>>>
>>> void UARTSettings(void);
>>>> Procedura odpowiedzialna za poprawne ustawienie USART-a 3, w naszym przypadku -  modu³u Wi-Fi ESP8266-01.
>>>
>>> void RelaysSettings(void);
>>>> Procedura, w której odpowiednio ustawiany jest przekaŸnik SRD-12VDC-SL-C.
>>>
>>> void ESPInitialization(void);
>>>> Procedura, w której przy pomocy modu³u Wi-Fi ESP8266-01 w pierwszej kolejnoœci roz³¹czamy jakiekolwiek po³¹czenia z Access Pointem (komenda: **AT+CWQAP**), jeœli takie istnia³o; nastêpnie ustawiamy tryb hosta (komenda: **AT+CWMODE=2**); w dalszej kolejnoœci pozwalamy na wiele po³¹czeñ (komenda: **AT+CIPMUX=1**); na sam koniec tworzymy server i przypisujemy go do portu 1234 (komenda: **AT+CIPSERVER=1,1234**). Wiêcej komend mo¿na znaleŸæ pod linkiem: [komendy AT](https://room-15.github.io/blog/2015/03/26/esp8266-at-command-reference/).
>>>
>>> void USART3_IRQHandler(void);
>>>> Procedura odpowiedzialna za obs³ugê przerwañ USART-a 3, w której dzieje siê ca³a 'magia'; odbierane s¹ wszystkie komendy i wywo³ywana jest procedura *handleMessage*.
>>>
>>> void handleMessage(void);
>>>> Wspomniana procedura wykorzystywana jest do poprawnego odczytywania odebranych komend, na podstawie których nastêpnie odpowiednio prze³¹czane s¹ prze³¹czniki.
>>>
>>> void sendMessage(void);
>>>> Procedura, odpowiedzialna za wysy³anie komend do USART-a 3 - modu³u Wi-Fi.
>>>
>>> void getRelay(void);
>>>> Procedura w której sprawdzany jest stan portów PD12 i PD13, zale¿nie od stanu odsy³ane s¹ ró¿ne komendy; wykorzystywana komenda: **AT+CIPSEND** z dwoma parametrami, z których pierwszy okreœla id wiadomoœci, drugi - jej d³ugoœæ, nastêpnie przy pomocy procedury *sendMessage* przesy³ana jest konkretna komenda...
>>>
>>> void setRelay(void);
>>>> ..., obs³ugiwana w *handleMessage*, która to wywo³uje procedurê *setRelay* odpowiedzialn¹ za w³aœciwe prze³¹czanie przekaŸników na podstawie wartoœci parametrów: *bit1* oraz *bit2*.
>>>
>>> uint16_t strLen(char *);
>>>> Funkcja pomocnicza, zwracaj¹ca d³ugoœæ, podanego jako parametr, ³añcucha znaków.
>>>
>>> void Delay(void);
>>>> Procedura pomocnicza, odpowiedzialna za opóŸnienie.
>
> \.\.\SmartGniazdko\SmartGniazdkoWinFormsApp\Windows app\ESP
>> Lokalizacja plików aplikacji, poœrednicz¹cej miêdzy u¿ytkownikiem a mikrokontrolerem.
>>> TCP.cs
>>>> Plik, w którym tworzony jest nowy klient.
>>>
>>> Program.cs
>>>> Plik, w którym wywo³ywana jest sama aplikacja oraz jej **zaawansowany** interfejs graficzny.
>>>
>>> MAIN_WINDOW.cs
>>>> Plik, w którym zapisana jest ca³a logika zwi¹zana z wysy³aniem wiadomoœci przez klienta na serwer.
>>>
>>> MAIN_WINDOW.Designer.cs
>>> Plik odpowiedzialny za ca³y wygl¹d aplikacji, wyœwietlanie placeholderów, buttonów, etc.

## Zdjêcia przedstawiaj¹ce efekt koñcowy

1. Pod³¹czenie modu³u Wi-Fi ESP8266-01 oraz przekaŸników SRD-12VDC-SL-C z p³ytk¹ STM32F407G Discovery
	
![final project](https://preview.ibb.co/cjGKLJ/4.jpg)

2.	Wygl¹d aplikacji s³u¿¹cej do komunikacji u¿ytkownik-mikrokontroler

![app](https://image.ibb.co/m49yfJ/35328010_1826042714119289_4439020950079406080_n.png)

## Credits
Micha³ Miroñczuk: [mirooon](https://github.com/mirooon),
Albert Millert: [devmood](https://github.com/devmood)

---
MIT Licence