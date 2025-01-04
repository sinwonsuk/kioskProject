# 키오스크 시스템

## 프로젝트 개요
이 프로젝트는 **음식 주문 키오스크** 시스템을 개발하는 것을 목표로 합니다.  
사용자는 키오스크를 통해 간단하게 메뉴를 선택하고, 주문과 결제를 진행할 수 있습니다.

### 주요 기능
- **메뉴 탐색 및 선택**: 다양한 메뉴를 확인하고 선택
- **로그인 및 회원가입**: 로그인,회원가입을 통해 메뉴 조절 가능 
- **결제 처리**: 
- **주문 관리**: 서버->db로 주문 정보 넘김

- ## 시스템 구조
키오스크는 **클라이언트, 서버, 데이터베이스**로 구성됩니다.

- **클라이언트**: 사용자 인터페이스(UI)를 제공하고 사용자 요청을 서버로 전달
- **서버**: 사용자 요청을 처리하고 데이터베이스와 통신
- **데이터베이스**: 사용자 정보, 메뉴 정보, 주문 내역을 저장

### 데이터 흐름
1. 사용자가 메뉴를 선택하고 주문 요청을 보냄
2. 서버가 요청을 처리하고 데이터베이스에서 정보를 조회
3. 주문 내역 및 결제 정보를 클라이언트로 반환

## 주요 기능 설명

### 1.메뉴 선택
- 사용자는 키오스크 화면에서 원하는 메뉴를 탐색하고 장바구니에 추가할 수 있습니다.
![스크린샷 2025-01-04 142437](https://github.com/user-attachments/assets/1cf5791a-3f0b-4eda-8e9f-c5fdfb726d00)

### 2. 결제
- 결제를 하면 서버에서 텍스트박스를 통해 주문 목록을 볼수있으며, db로 정보가 넘어갑니다. 
![스크린샷 2025-01-04 150108](https://github.com/user-attachments/assets/c4f594c6-01d5-43ce-88d9-5715cd3e5ca7)

### 3. 로그인
- 클라에서 로그인을 요청하면 서버를 통해 db에서 정보를 받아와 로그인을 합니다. 
![스크린샷 2025-01-04 150108](https://github.com/user-attachments/assets/c4f594c6-01d5-43ce-88d9-5715cd3e5ca7)

### 3. 관리자 기능
- 관리자 계정을 통해 메뉴를 추가/수정/삭제할 수 있습니다.
![스크린샷 2025-01-04 145637](https://github.com/user-attachments/assets/3ffffd77-ff41-42b5-8110-ecdc2de82b0b)
