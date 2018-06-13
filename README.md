# SmartPlug

## Zasada dzia�ania oraz kr�tki opis

**SmartPlug** jest projektem zaliczeniowym na zaj�cia z Podstawy Techniki Mikroprocerowej, napisanym w j�zyku C. Dodatkowo, aplikacja s�u��ca do komunikacji mi�dzy u�ytkownikiem a urz�dzeniem, zosta�a napisana w C#. SmartPlug dzia�a na prostej zasadzie:

- urz�dzenie elektroniczne (TV, lampka, suszarka, etc.) zostaje pod��czane do 'gniazdka',

- gniazdko 'tworzy' w�asny hotspot - otwarta sie� **AI-THINKER_F68149**,

- mo�na si� do niego pod��czy� za pomoc� interfejsu aplikacji, nale�y r�wnie� poda� przyk�adowo Adres: 192.168.4.1 oraz nr Portu: 1234,

- dodatkowo aplikacja umo�liwia prze��czanie przeka�nik�w,

- przeka�niki po przekroczeniu wymaganej warto�ci, prze��czaj� si� w spos�b skokowy, umo�liwiaj�c dop�yw pr�du lub jego odci�cie, dzi�ki czemu urz�dzenie elektroniczne mo�e si� w��czy� lub wy��czy�.

## Wykorzystywane podzespo�y

1.	STM32F407G Discovery

![stm32f4](https://image.ibb.co/kWcQSy/stm32f4_discovery.jpg)

2.	Modu� Wi-Fi ESP8266-01

![esp](https://image.ibb.co/mrpVSy/HCMODU0085_800_600_New.jpg)

4.	Przeka�nik SRD-12VDC-SL-C

![relay](https://image.ibb.co/jzYAud/relay_module_12v_1024x1024.jpg)

## Spos�b pod��czenia pin�w

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
>>	Lokalizacja pliku *main.c*, w kt�rym zawarta jest ca�a logika projektu, realizowana na mikrokontrolerze.
>>
>>> int main(void);
>>>> Funkcja zawieraj�ca p�tl� niesko�czon�, wywo�ywane s� w niej r�wnie� inne procedury oraz funkcje.
>>>
>>> void UARTSettings(void);
>>>> Procedura odpowiedzialna za poprawne ustawienie USART-a 3, w naszym przypadku -  modu�u Wi-Fi ESP8266-01.
>>>
>>> void RelaysSettings(void);
>>>> Procedura, w kt�rej odpowiednio ustawiany jest przeka�nik SRD-12VDC-SL-C.
>>>
>>> void ESPInitialization(void);
>>>> Procedura, w kt�rej przy pomocy modu�u Wi-Fi ESP8266-01 w pierwszej kolejno�ci roz��czamy jakiekolwiek po��czenia z Access Pointem (komenda: **AT+CWQAP**), je�li takie istnia�o; nast�pnie ustawiamy tryb hosta (komenda: **AT+CWMODE=2**); w dalszej kolejno�ci pozwalamy na wiele po��cze� (komenda: **AT+CIPMUX=1**); na sam koniec tworzymy server i przypisujemy go do portu 1234 (komenda: **AT+CIPSERVER=1,1234**). Wi�cej komend mo�na znale�� pod linkiem: [komendy AT](https://room-15.github.io/blog/2015/03/26/esp8266-at-command-reference/).
>>>
>>> void USART3_IRQHandler(void);
>>>> Procedura odpowiedzialna za obs�ug� przerwa� USART-a 3, w kt�rej dzieje si� ca�a 'magia'; odbierane s� wszystkie komendy i wywo�ywana jest procedura *handleMessage*.
>>>
>>> void handleMessage(void);
>>>> Wspomniana procedura wykorzystywana jest do poprawnego odczytywania odebranych komend, na podstawie kt�rych nast�pnie odpowiednio prze��czane s� prze��czniki.
>>>
>>> void sendMessage(void);
>>>> Procedura, odpowiedzialna za wysy�anie komend do USART-a 3 - modu�u Wi-Fi.
>>>
>>> void getRelay(void);
>>>> Procedura w kt�rej sprawdzany jest stan port�w PD12 i PD13, zale�nie od stanu odsy�ane s� r�ne komendy; wykorzystywana komenda: **AT+CIPSEND** z dwoma parametrami, z kt�rych pierwszy okre�la id wiadomo�ci, drugi - jej d�ugo��, nast�pnie przy pomocy procedury *sendMessage* przesy�ana jest konkretna komenda...
>>>
>>> void setRelay(void);
>>>> ..., obs�ugiwana w *handleMessage*, kt�ra to wywo�uje procedur� *setRelay* odpowiedzialn� za w�a�ciwe prze��czanie przeka�nik�w na podstawie warto�ci parametr�w: *bit1* oraz *bit2*.
>>>
>>> uint16_t strLen(char *);
>>>> Funkcja pomocnicza, zwracaj�ca d�ugo��, podanego jako parametr, �a�cucha znak�w.
>>>
>>> void Delay(void);
>>>> Procedura pomocnicza, odpowiedzialna za op�nienie.
>
> \.\.\SmartGniazdko\SmartGniazdkoWinFormsApp\Windows app\ESP
>> Lokalizacja plik�w aplikacji, po�rednicz�cej mi�dzy u�ytkownikiem a mikrokontrolerem.
>>> TCP.cs
>>>> Plik, w kt�rym tworzony jest nowy klient.
>>>
>>> Program.cs
>>>> Plik, w kt�rym wywo�ywana jest sama aplikacja oraz jej **zaawansowany** interfejs graficzny.
>>>
>>> MAIN_WINDOW.cs
>>>> Plik, w kt�rym zapisana jest ca�a logika zwi�zana z wysy�aniem wiadomo�ci przez klienta na serwer.
>>>
>>> MAIN_WINDOW.Designer.cs
>>> Plik odpowiedzialny za ca�y wygl�d aplikacji, wy�wietlanie placeholder�w, button�w, etc.

## Zdj�cia przedstawiaj�ce efekt ko�cowy

1. Pod��czenie modu�u Wi-Fi ESP8266-01 oraz przeka�nik�w SRD-12VDC-SL-C z p�ytk� STM32F407G Discovery
	
![final project](https://preview.ibb.co/cjGKLJ/4.jpg)

2.	Wygl�d aplikacji s�u��cej do komunikacji u�ytkownik-mikrokontroler

![app](https://image.ibb.co/m49yfJ/35328010_1826042714119289_4439020950079406080_n.png)

## Credits
Micha� Miro�czuk: [mirooon](https://github.com/mirooon),
Albert Millert: [devmood](https://github.com/devmood)

---
MIT Licence