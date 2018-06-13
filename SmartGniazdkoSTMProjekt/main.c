#include "misc.h"
#include "stm32f4xx_exti.h"
#include "stm32f4xx_gpio.h"
#include "stm32f4xx_rcc.h"
#include "stm32f4xx_syscfg.h"
#include "stm32f4xx_usart.h"
#include "stm32f4xx_conf.h"
#include <string.h>

#define messageSize 64

void USART3_IRQHandler(void);
int main(void);
void UARTSettings(void);
void RelaysSettings(void);
void ESPInitialization(void);
void handleMessage(char *);
void setRelay(uint8_t, uint8_t);
void getRelay(void);
void sendMessage(char*);
uint16_t strLen(char *);
void Delay(uint32_t);

uint8_t receiveMessagePos = 0;
char receiveMessage[messageSize] = {0};

void USART3_IRQHandler(void){

	uint16_t buffer = 0;

	if(USART_GetITStatus(USART3, USART_IT_RXNE) != RESET){
		buffer = USART3->DR;
		receiveMessage[receiveMessagePos] = (char)buffer;

		if(buffer == '\n')
		{
			handleMessage(receiveMessage);
			for(uint8_t i = 0; i<messageSize; i++)
			{
					receiveMessage[i] = 0;
				}
				receiveMessagePos = 0;
		} else
		{
			receiveMessagePos++;
		}
	}
}

int main(void){
	SystemInit();
	GPIOInit();
	UARTSettings();
	RelaysSettings();
	ESPInitialization();

	while(1)
	{
		
	}
}
void GPIOInit(void)
{
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOD, ENABLE);

	GPIO_InitTypeDef GPIO_InitStructure;
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_12 | GPIO_Pin_13 | GPIO_Pin_14 | GPIO_Pin_15;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_OUT;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_100MHz;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_NOPULL;
	GPIO_Init(GPIOD, &GPIO_InitStructure);

	GPIO_ResetBits(GPIOD, GPIO_Pin_12);
	GPIO_ResetBits(GPIOD, GPIO_Pin_13);
}

void UARTSettings(void){
	NVIC_PriorityGroupConfig(NVIC_PriorityGroup_1);

	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOC, ENABLE);
	RCC_APB1PeriphClockCmd(RCC_APB1Periph_USART3, ENABLE);

	GPIO_InitTypeDef GPIO_InitStructure;
	GPIO_InitStructure.GPIO_Pin = GPIO_Pin_10 | GPIO_Pin_11;
	GPIO_InitStructure.GPIO_Mode = GPIO_Mode_AF;
	GPIO_InitStructure.GPIO_OType = GPIO_OType_PP;
	GPIO_InitStructure.GPIO_PuPd = GPIO_PuPd_UP;
	GPIO_InitStructure.GPIO_Speed = GPIO_Speed_50MHz;
	GPIO_Init(GPIOC, &GPIO_InitStructure);

	GPIO_PinAFConfig(GPIOC, GPIO_PinSource10, GPIO_AF_USART3);
	GPIO_PinAFConfig(GPIOC, GPIO_PinSource11, GPIO_AF_USART3);

	USART_InitTypeDef USART_InitStructure;
	USART_InitStructure.USART_BaudRate = 115200;
	USART_InitStructure.USART_WordLength = USART_WordLength_8b;
	USART_InitStructure.USART_StopBits = USART_StopBits_1;
	USART_InitStructure.USART_Parity = USART_Parity_No;
	USART_InitStructure.USART_HardwareFlowControl = USART_HardwareFlowControl_None;
	USART_InitStructure.USART_Mode = USART_Mode_Rx | USART_Mode_Tx;
	USART_Init(USART3, &USART_InitStructure);

	NVIC_InitTypeDef NVIC_InitStructure;
	USART_ITConfig(USART3, USART_IT_RXNE, ENABLE);
	NVIC_InitStructure.NVIC_IRQChannel = USART3_IRQn;
	NVIC_InitStructure.NVIC_IRQChannelPreemptionPriority = 0x01;
	NVIC_InitStructure.NVIC_IRQChannelSubPriority = 0x01;
	NVIC_InitStructure.NVIC_IRQChannelCmd = ENABLE;
	NVIC_Init(&NVIC_InitStructure);

	NVIC_EnableIRQ(USART3_IRQn);
	USART_Cmd(USART3, ENABLE);
}

void RelaysSettings(){
	RCC_AHB1PeriphClockCmd(RCC_AHB1Periph_GPIOE, ENABLE);

	GPIO_InitTypeDef GPIO_Rel_IS;
	GPIO_Rel_IS.GPIO_Pin = GPIO_Pin_12 | GPIO_Pin_13;
	GPIO_Rel_IS.GPIO_Mode = GPIO_Mode_OUT;
	GPIO_Rel_IS.GPIO_OType = GPIO_OType_OD;
	GPIO_Rel_IS.GPIO_Speed = GPIO_Speed_100MHz;
	GPIO_Rel_IS.GPIO_PuPd = GPIO_PuPd_DOWN;
	GPIO_Init(GPIOE, &GPIO_Rel_IS);

	GPIO_SetBits(GPIOE, GPIO_Pin_12);
	GPIO_SetBits(GPIOE, GPIO_Pin_13);
}

void handleMessage(char *message){
	char a[1] = {0};
	char b[1] = {0};
	int a1 = 0;
	int b1 = 0;
	uint16_t messageLen = strLen(message);

	Delay(5);
	if(strstr(message, "getRelay()") != NULL)
	{
		getRelay();
		//GPIO_ToggleBits(GPIOD, GPIO_Pin_15);
	}
	else if(strstr(message, "setRelay(") != NULL)
	{
		a[0] = message[messageLen-5];
		b[0] = message[messageLen-4];
		a1 = atoi(a);
		b1 = atoi(b);
		GPIO_ToggleBits(GPIOD, GPIO_Pin_15);
		setRelay(a1, b1);
	}
}

void ESPInitialization(void){
	Delay(200);
	sendMessage("AT+CWQAP\r\n");
	Delay(200);
	sendMessage("AT+CWMODE=2\r\n");
	Delay(200);
	sendMessage("AT+CIPMUX=1\r\n");
	Delay(200);
	sendMessage("AT+CIPSERVER=1,1234\r\n");
	Delay(200);
}

void Delay(volatile uint32_t i){
	i *= 24;
	while(i--);
}

void sendMessage(char message[messageSize]){
	for(uint8_t i=0; message[i] != '\0'; i++)
	{
		USART_SendData(USART3, message[i]);
		Delay(500);
	}
}

uint16_t strLen(char *text){
	uint16_t i = 0;
	while(text[i] != '\n')
	{
		i++;
	}
	return (i+1);
}

void getRelay(){
	if(!GPIO_ReadOutputDataBit(GPIOE, GPIO_Pin_12))
	{
		if(!GPIO_ReadOutputDataBit(GPIOE, GPIO_Pin_13))
		{
			sendMessage("AT+CIPSEND=0,3\r\n");
			Delay(200);
			sendMessage("11\n");
		}
		else
		{
			sendMessage("AT+CIPSEND=0,3\r\n");
			Delay(200);
			sendMessage("10\n");
		}
	}
	else
	{
		if(!GPIO_ReadOutputDataBit(GPIOE, GPIO_Pin_13))
		{
			sendMessage("AT+CIPSEND=0,3\r\n");
			Delay(10);
			sendMessage("01\n");
		}
		else
		{
			sendMessage("AT+CIPSEND=0,3\r\n");
			Delay(10);
			sendMessage("00\n");
		}
	}
}


void setRelay(uint8_t bit1, uint8_t bit2)
{
	if(bit1)
	{
		sendMessage("AT+CIPSEND=0,19\r\n");
		Delay(200);
		GPIO_ToggleBits(GPIOD, GPIO_Pin_13);
		GPIO_ToggleBits(GPIOE, GPIO_Pin_12);
	}
	if(bit2)
	{
		sendMessage("AT+CIPSEND=0,19\r\n");
		Delay(200);

		GPIO_ToggleBits(GPIOD, GPIO_Pin_12);
		GPIO_ToggleBits(GPIOE, GPIO_Pin_13);
	}

}
